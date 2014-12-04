using System;

namespace SoftCareManager.Contracts.Authorization
{
    [Flags]
    public enum Role
    {
        IT_Developert = 0,
        IT_Service = 1,
        TherapyConsult = 2,
        CareManager = 4
    }
}