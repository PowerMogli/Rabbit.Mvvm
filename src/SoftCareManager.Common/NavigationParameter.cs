using System;
using System.Collections.Generic;
using System.Linq;

using SoftCareManager.Contracts.Application.Navigation;

namespace SoftCareManager.Common
{
    public class NavigationParameter : INavigationParameter
    {
        public NavigationParameter(string regionName, Type viewModelType)
        {
            RegionName = regionName;
            ViewModelType = viewModelType;
        }

        protected NavigationParameter()
        {
        }

        public Dictionary<string, object> Parameter { get; private set; }

        public string RegionName { get; private set; }

        public virtual int ShellId { get; protected set; }

        public bool StayVisible { get; private set; }

        public Type ViewModelType { get; private set; }

        public INavigationParameter SetParameter(KeyValuePair<string, object>[] parameter)
        {
            Parameter = parameter.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return this;
        }

        public INavigationParameter SetShellId(int shellId)
        {
            ShellId = shellId;

            return this;
        }

        public INavigationParameter SetStayVisible(bool stayVisible)
        {
            StayVisible = stayVisible;

            return this;
        }
    }
}