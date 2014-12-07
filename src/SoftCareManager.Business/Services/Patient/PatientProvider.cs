using System;
using System.Collections.Generic;
using System.Linq;

using SoftCareManager.Business.Model.Patient;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.Services;

namespace SoftCareManager.Business.Services.Patient
{
    [Service(ServiceName = Service.PatientProvider)]
    public class PatientProvider : IPatientProvider
    {
        public IEnumerable<IPatientModel> GetPatients(string netzwerk)
        {
            switch (netzwerk)
            {
                case "naip:Berlin":
                    return GetPatientsFromBerlin();
                case "naip:Nürnberg":
                    return GetPatientsFromNuremberg();
                default:
                    return Enumerable.Empty<IPatientModel>();
            }
        }

        private IEnumerable<IPatientModel> GetPatientsFromBerlin()
        {
            return new List<IPatientModel>
            {
                new PatientModel
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Albert",
                    LastName = "Slupianek",
                    Birthday = new DateTime(1982, 5, 20)
                },
                new PatientModel
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Gerd",
                    LastName = "Müller",
                    Birthday = new DateTime(1954, 7, 1)
                }
            };
        }

        private IEnumerable<IPatientModel> GetPatientsFromNuremberg()
        {
            return new List<IPatientModel>
            {
                new PatientModel
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Stefan",
                    LastName = "Wursthuber",
                    Birthday = new DateTime(1923, 6, 21)
                },
                new PatientModel
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Manfred",
                    LastName = "Gruber",
                    Birthday = new DateTime(1973, 10, 19)
                }
            };
        }
    }
}