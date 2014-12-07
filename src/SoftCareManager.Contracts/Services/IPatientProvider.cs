using System.Collections.Generic;

using SoftCareManager.Contracts.Model.Patient;

namespace SoftCareManager.Contracts.Services
{
    public interface IPatientProvider : IService
    {
        IEnumerable<IPatientModel> GetPatients(string netzwerk);
    }
}