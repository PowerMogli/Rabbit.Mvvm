using System;

namespace SoftCareManager.Contracts.Authorization
{
    [Flags]
    public enum Role
    {
        None = 0,
        ItDeveloper = 1,
        ItService = 2,
        TherapyConsult = 4,
        CareManager = 8
    }
}