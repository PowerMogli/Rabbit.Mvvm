using SoftCareManager.Contracts.Model.Patient;
using System.Collections.Generic;

namespace SoftCareManager.Contracts.Services
{
    public interface IPatientProvider : IService
    {
        IEnumerable<IPatientModel> GetPatients(string netzwerk);
    }
}