using SoftCareManager.Contracts.Application.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftCareManager.Common
{
    public class NavigationParameter : INavigationParameter
    {
        protected NavigationParameter()
        {

        }

        public NavigationParameter(string regionName, Type viewModelType)
        {
            RegionName = regionName;
            ViewModelType = viewModelType;
        }

        public INavigationParameter SetStayVisible(bool stayVisible)
        {
            StayVisible = stayVisible;

            return this;
        }

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

        public string RegionName { get; private set; }

        public Type ViewModelType { get; private set; }

        public Dictionary<string, object> Parameter { get; private set; }

        public bool StayVisible { get; private set; }

        public virtual int ShellId { get; protected set; }
    }
}