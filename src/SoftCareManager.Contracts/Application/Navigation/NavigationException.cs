using System;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public class NavigationException : Exception
    {
        public NavigationException(string message)
            : this(message, null) { }

        public NavigationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}