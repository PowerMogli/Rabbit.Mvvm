﻿using System;
using System.Linq.Expressions;

namespace SoftCareManager.Common
{
    public static class Guard
    {
        public static void ArgumentIsNotNull<TArgument>(this TArgument argument, Expression<Func<TArgument>> argumentLambda)
        {
            bool throwException = false;
            if (argument is string)
            {
                throwException = string.IsNullOrWhiteSpace(argument as string);
            }
            else
            {
                throwException = argument == null;
            }

            if (throwException)
            {
                throw new ArgumentNullException(GetPropertyName(argumentLambda), "Argument cannot be null!");
            }
        }

        private static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            MemberExpression memberExpression = propertyLambda.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return memberExpression.Member.Name;
        }
    }
}