using SoftCareManager.Contracts.WorkItems;
using System;

namespace SoftCareManager.Contracts
{
    public interface IObjectBuilder
    {
        object Build(Type typeToBuild, IAppController appWorkItem);
    }
}