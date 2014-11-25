
namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface INavigationAware
    {
        bool NavigateFrom();

        void NavigateTo();
    }
}