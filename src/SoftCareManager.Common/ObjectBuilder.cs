using SoftCareManager.Contracts;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.WorkItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SoftCareManager.Common
{
    delegate T ObjectActivator<T>(params object[] args);

    delegate object ObjectActivator(params object[] args);

    [Export(typeof(IObjectBuilder))]
    public class ObjectBuilder : IObjectBuilder
    {
        private IDictionary<Type, Expression<ObjectActivator>> objectFactories;

        public ObjectBuilder()
        {
            objectFactories = new Dictionary<Type, Expression<ObjectActivator>>();
        }

        public object Build(Type typeToBuild, IAppController appController)
        {
            ConstructorInfo ctor = typeToBuild.GetConstructors().First();
            ObjectActivator createdActivator = (ObjectActivator)GetActivator<ObjectActivator>(ctor);

            return createdActivator(GetParameter(ctor, appController));
        }

        public T Build<T>(IAppController appController)
        {
            ConstructorInfo ctor = typeof(T).GetConstructors().First();
            ObjectActivator<T> createdActivator = (ObjectActivator<T>)GetActivator<ObjectActivator<T>>(ctor);

            return createdActivator(GetParameter(ctor, appController));
        }

        private object[] GetParameter(ConstructorInfo ctor, IAppController appController)
        {
            var parameterInfos = ctor.GetParameters();
            object[] args = new object[parameterInfos.Length];

            for (var index = 0; index <= parameterInfos.Length - 1; index++)
            {
                var parameterType = parameterInfos[index].ParameterType;

                if (parameterType.IsAssignableFrom(typeof(IService)))
                {
                    args[index] = appController.GetService(parameterType);
                }
                else if (parameterType.IsAssignableFrom(typeof(IWorkItem)))
                {
                    args[index] = appController.GetWorkItem(parameterType);
                }
                else if (parameterType.IsAssignableFrom(typeof(IAppController)))
                {
                    args[index] = appController;
                }
            }

            return args;
        }

        private static Delegate GetActivator<TActivator>(ConstructorInfo ctor)
        {
            ParameterInfo[] parameterInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExpression = CreateParameterExpression(parameterInfo, parameterExpression);

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExpression = Expression.New(ctor, argsExpression);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambdaExpression = Expression.Lambda(typeof(TActivator), newExpression, parameterExpression);

            return lambdaExpression.Compile();
        }

        private static Expression[] CreateParameterExpression(ParameterInfo[] paramsInfo, ParameterExpression param)
        {
            Expression[] argsExpression = new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExpression = Expression.ArrayIndex(param, index);

                Expression paramCastExpression = Expression.Convert(paramAccessorExpression, paramType);

                argsExpression[i] = paramCastExpression;
            }
            return argsExpression;
        }
    }
}