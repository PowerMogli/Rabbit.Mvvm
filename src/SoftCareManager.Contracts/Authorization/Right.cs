using System;

namespace SoftCareManager.Contracts.Authorization
{
    [Flags]
    public enum Right
    {
        // Patient
        AddPatient = 0,
        DeletePatient = 1,
        EditPatient = 2,

        // Therapy
        AddTherapy = 4,
        DeleteTherapy = 8,
        EditTherapy = 16
    }
}