using SoftCareManager.Contracts.Application.Navigation;
using System;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public class NavigationResult : INavigationResult
    {
        public NavigationResult(bool? result)
            : this(result, null) { }

        public NavigationResult(bool? result, NavigationException exception)
        {
            this.Result = result;
            this.Exception = exception;
        }

        public bool? Result { get; private set; }

        public NavigationException Exception { get; private set; }
    }
}