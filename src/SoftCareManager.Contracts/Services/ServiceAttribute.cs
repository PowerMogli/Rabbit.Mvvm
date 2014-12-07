using System;
using System.ComponentModel.Composition;

namespace SoftCareManager.Contracts.Services
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : InheritedExportAttribute
    {
        public ServiceAttribute()
            : base(typeof (IService))
        {
        }

        public string ServiceName { get; set; }

        public Type ServiceType { get; set; }
    }
}