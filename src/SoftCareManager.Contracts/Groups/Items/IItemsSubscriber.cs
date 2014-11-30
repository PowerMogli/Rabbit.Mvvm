using SoftCareManager.Contracts.Model.Patient;
using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Groups.Items
{
    public interface IItemsSubscriber
    {
        ObservableCollection<IPatientModel> Items { get; set; }
    }
}