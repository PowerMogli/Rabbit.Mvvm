namespace SoftCareManager.Common
{
    internal class ObjectCreationInfo
    {
        private readonly ObjectActivator _objectActivator;
        private readonly object[] _parameters;

        internal ObjectCreationInfo(ObjectActivator objectActivator, object[] parameters)
        {
            _objectActivator = objectActivator;
            _parameters = parameters;
        }

        internal object InvokeActivator()
        {
            return _objectActivator.Invoke(_parameters);
        }
    }
}