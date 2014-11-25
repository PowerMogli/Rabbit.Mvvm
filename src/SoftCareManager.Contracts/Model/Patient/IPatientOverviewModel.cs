using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Model.Patient
{
    public interface IPatientOverviewModel : IModel
    {
        ObservableCollection<IPatientModel> Patients { get; }
    }
}