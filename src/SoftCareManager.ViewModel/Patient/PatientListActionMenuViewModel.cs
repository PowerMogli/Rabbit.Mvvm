using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientListActionMenuViewModel : ViewModelBase, ISelectionSubscriber, IItemsSubscriber
    {
        public override void Initialize(INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            CanBeActivated = true;
        }

        public bool CanEdit()
        {
            return SelectedItem != null;
        }

        public void Edit()
        {
            SelectedItem.LastName = "Test";
        }

        public bool CanCreate()
        {
            return Items != null;
        }

        public void Create()
        {
            Items.Add(new PatientViewModel(null, null, null) { FirstName = "Franz", LastName = "Beckenbauer", Birthday = new DateTime(1943, 4, 20) });
        }

        public IPatientModel SelectedItem { get; set; }

        public ObservableCollection<IPatientModel> Items { get; set; }
    }
}