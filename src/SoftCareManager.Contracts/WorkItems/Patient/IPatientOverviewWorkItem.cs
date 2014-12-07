using System.Collections.Generic;

using SoftCareManager.Contracts.Model.Patient;

namespace SoftCareManager.Contracts.WorkItems.Patient
{
    public interface IPatientOverviewWorkItem : IWorkItem
    {
        IEnumerable<IPatientModel> GetPatients(string network);
    }
}