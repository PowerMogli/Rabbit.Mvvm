namespace SoftCareManager.Contracts.Application.Navigation
{
    public class NavigationResult : INavigationResult
    {
        public NavigationResult(bool? result)
            : this(result, null)
        {
        }

        public NavigationResult(bool? result, NavigationException exception)
        {
            Result = result;
            Exception = exception;
        }

        public NavigationException Exception { get; private set; }

        public bool? Result { get; private set; }
    }
}