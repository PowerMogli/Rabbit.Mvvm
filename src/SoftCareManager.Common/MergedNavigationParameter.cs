using System.Collections.ObjectModel;

using SoftCareManager.Contracts.Application.Navigation;

namespace SoftCareManager.Common
{
    public class MergedNavigationParameter : NavigationParameter, IMergedNavigationParameter
    {
        private int _shellId;

        public MergedNavigationParameter()
        {
            NavigationParameter = new Collection<INavigationParameter>();
        }

        public Collection<INavigationParameter> NavigationParameter { get; private set; }

        public override int ShellId
        {
            get { return _shellId; }
            protected set
            {
                _shellId = value;

                foreach (var navigationParameter in NavigationParameter)
                {
                    navigationParameter.SetShellId(_shellId);
                }
            }
        }

        public void Add(INavigationParameter navigationParameter)
        {
            NavigationParameter.Add(navigationParameter);
        }
    }
}