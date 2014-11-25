using SoftCareManager.Contracts.WorkItems;
using System;

namespace SoftCareManager.Contracts
{
    public interface IObjectBuilder
    {
        T Build<T>(IAppController appWorkItem);

        object Build(Type typeToBuild, IAppController appWorkItem);
    }
}