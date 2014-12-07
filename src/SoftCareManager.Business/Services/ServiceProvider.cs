using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

using Service = SoftCareManager.Contracts.Services;

namespace SoftCareManager.Business.Services
{
    [Export(typeof (Service.IServiceProvider))]
    public class ServiceProvider : Service.IServiceProvider
    {
        [ImportMany]
        private IEnumerable<Lazy<Service.IService, Service.IServiceMetaData>> Services { get; set; }

        public TService GetService<TService>() where TService : Service.IService
        {
            Lazy<Service.IService, Service.IServiceMetaData> exportedService = (from service in Services
                                                                                where service.Metadata.ServiceName.Equals(typeof (TService).Name)
                                                                                select service).FirstOrDefault();

            return exportedService != null
                ? (TService)exportedService.Value
                : default(TService);
        }

        public object GetService(Type serviceType)
        {
            Lazy<Service.IService, Service.IServiceMetaData> exportedService = (from service in Services
                                                                                where service.Metadata.ServiceType == serviceType
                                                                                select service).FirstOrDefault();

            return exportedService != null
                ? exportedService.Value
                : null;
        }
    }
}