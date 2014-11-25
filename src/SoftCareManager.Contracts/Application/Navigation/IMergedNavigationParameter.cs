using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface IMergedNavigationParameter
    {
        void Add(INavigationParameter navigationParameter);

        Collection<INavigationParameter> NavigationParameter { get; }
    }
}