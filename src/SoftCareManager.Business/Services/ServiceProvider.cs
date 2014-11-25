using SoftCareManager.Contracts.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Service = SoftCareManager.Contracts.Services;

namespace SoftCareManager.Business.Services
{
    [Export(typeof(Service.IServiceProvider))]
    public class ServiceProvider : Service.IServiceProvider
    {
        [ImportMany]
        IEnumerable<Lazy<IService, IServiceMetaData>> Services { get; set; }

        public TService GetService<TService>() where TService : IService
        {
            var exportedService = (from service in Services
                                   where service.Metadata.ServiceName.Equals(typeof(TService).Name)
                                   select service).FirstOrDefault();

            return (TService)exportedService.Value;
        }

        public object GetService(Type serviceType)
        {
            var exportedService = (from service in Services
                                   where service.Metadata.ServiceType.Equals(serviceType)
                                   select service).FirstOrDefault();

            return exportedService.Value;
        }
    }
}