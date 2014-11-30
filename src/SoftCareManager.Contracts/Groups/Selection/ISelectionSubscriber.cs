using SoftCareManager.Contracts.Model.Patient;

namespace SoftCareManager.Contracts.Groups.Selection
{
    public interface ISelectionSubscriber
    {
        IPatientModel SelectedItem { get; set; }
    }
}