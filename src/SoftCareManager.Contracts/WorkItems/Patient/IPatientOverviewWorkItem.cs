using SoftCareManager.Contracts.Model.Patient;
using System.Collections.Generic;

namespace SoftCareManager.Contracts.WorkItems.Patient
{
    public interface IPatientOverviewWorkItem : IWorkItem
    {
        IEnumerable<IPatientModel> GetPatients(string network);
    }
}