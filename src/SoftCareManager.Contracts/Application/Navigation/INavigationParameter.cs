using System;
using System.Collections.Generic;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface INavigationParameter
    {
        bool StayVisible { get; }

        int ShellId { get; }

        string RegionName { get; }

        Type ViewModelType { get; }

        Dictionary<string, object> Parameter { get; }

        INavigationParameter SetStayVisible(bool stayVisible);

        INavigationParameter SetParameter(KeyValuePair<string, object>[] parameter);

        INavigationParameter SetShellId(int shellId);
    }
}