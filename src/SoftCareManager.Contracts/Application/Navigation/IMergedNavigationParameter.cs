using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface IMergedNavigationParameter
    {
        Collection<INavigationParameter> NavigationParameter { get; }

        void Add(INavigationParameter navigationParameter);
    }
}