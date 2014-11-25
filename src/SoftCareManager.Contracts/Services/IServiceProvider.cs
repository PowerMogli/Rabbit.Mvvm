using System;

namespace SoftCareManager.Contracts.Services
{
    public interface IServiceProvider
    {
        TService GetService<TService>() where TService : IService;

        object GetService(Type serviceType);
    }
}