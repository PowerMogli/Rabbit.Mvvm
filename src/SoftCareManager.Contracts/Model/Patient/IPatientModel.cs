using System;

namespace SoftCareManager.Contracts.Model.Patient
{
    public interface IPatientModel : IModel
    {
        string LastName { get; set; }

        string FirstName { get; set; }

        DateTime Birthday { get; set; }
    }
}