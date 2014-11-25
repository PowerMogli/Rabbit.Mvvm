using SoftCareManager.Contracts.Application.Navigation;
using System.Collections.ObjectModel;

namespace SoftCareManager.Common
{
    public class MergedNavigationParameter : NavigationParameter, IMergedNavigationParameter
    {
        public MergedNavigationParameter()
        {
            NavigationParameter = new Collection<INavigationParameter>();
        }

        public void Add(INavigationParameter navigationParameter)
        {
            NavigationParameter.Add(navigationParameter);
        }

        public Collection<INavigationParameter> NavigationParameter { get; private set; }

        private int shellId;
        public override int ShellId
        {
            get { return shellId; }
            protected set
            {
                shellId = value;

                foreach (var navigationParameter in NavigationParameter)
                {
                    navigationParameter.SetShellId(shellId);
                }
            }
        }
    }
}