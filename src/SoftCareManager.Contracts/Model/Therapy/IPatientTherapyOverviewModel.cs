using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Model.Therapy
{
    public interface IPatientTherapyOverviewModel : IModel
    {
        ObservableCollection<ITherapyModel> Therapies { get; }
    }
}