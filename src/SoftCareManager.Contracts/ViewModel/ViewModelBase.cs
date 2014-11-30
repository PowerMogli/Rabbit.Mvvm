using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;

namespace SoftCareManager.Contracts.ViewModel
{
    public abstract class ViewModelBase : ObservableObject
    {
        public bool CanBeActivated { get; protected set; }

        protected bool isInitialized;

        public virtual void Initialize(INavigationParameter navigationParameter)
        {
            if (isInitialized)
            {
                return;
            }

            InnerInitialize(navigationParameter);
        }

        protected virtual void InnerInitialize(INavigationParameter parameter)
        {

        }
    }
}