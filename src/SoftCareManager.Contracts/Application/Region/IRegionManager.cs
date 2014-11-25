using System;
using System.Collections.Generic;

namespace SoftCareManager.Contracts.Application.Region
{
    public interface IRegionManager
    {
        List<IRegion> Regions { get; set; }

        void Update();
    }
}