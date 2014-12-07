using System;

namespace SoftCareManager.Contracts.Services
{
    public interface IServiceMetaData
    {
        string ServiceName { get; }

        Type ServiceType { get; }
    }
}