using System;

namespace SoftCareManager.Contracts.Authorization
{
    [Flags]
    public enum Right
    {
        // Patient
        None = 0,
        AddPatient = 1,
        DeletePatient = 2,
        EditPatient = 4,

        // Therapy
        AddTherapy = 8,
        DeleteTherapy = 16,
        EditTherapy = 32
    }
}