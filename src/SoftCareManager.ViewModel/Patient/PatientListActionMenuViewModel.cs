using System;
using System.Collections.ObjectModel;

using SoftCareManager.Business.WorkItems.Patient;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientListActionMenuViewModel : ViewModelBase, ISelectionSubscriber<IPatientModel>, IItemsSubscriber
    {
        public ObservableCollection<object> Items { get; set; }

        public IPatientModel SelectedItem { get; set; }

        #region ISelectionSubscriber

        object ISelectionSubscriber.SelectedItem
        {
            get { return SelectedItem; }
            set { SelectedItem = (IPatientModel)value; }
        }

        #endregion

        public override void Initialize(INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            CanBeActivated = true;
        }

        public bool CanCreate()
        {
            return Items != null;
        }

        public bool CanEdit()
        {
            return SelectedItem != null;
        }

        public void Create()
        {
            Items.Add(new PatientViewModel(null, null, null, new PatientTherapyWorkItem())
            {
                FirstName = "Franz",
                LastName = "Beckenbauer",
                Birthday = new DateTime(1943, 4, 20)
            });
        }

        public void Edit()
        {
            SelectedItem.LastName = "Test";
        }
    }
}