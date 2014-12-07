namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface INavigationResult
    {
        NavigationException Exception { get; }

        bool? Result { get; }
    }
}