using System;

using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.Contracts
{
    public interface IObjectBuilder
    {
        object Build(Type typeToBuild, IAppController appWorkItem);
    }
}