using System;
using System.Collections.Generic;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface INavigationParameter
    {
        Dictionary<string, object> Parameter { get; }

        string RegionName { get; }

        int ShellId { get; }

        bool StayVisible { get; }

        Type ViewModelType { get; }

        INavigationParameter SetParameter(KeyValuePair<string, object>[] parameter);

        INavigationParameter SetShellId(int shellId);

        INavigationParameter SetStayVisible(bool stayVisible);
    }
}