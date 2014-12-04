using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.ViewModel.Patient;
using System.Collections.ObjectModel;

namespace SoftCareManager.ViewModel.Therapy
{
    public class PatientTherapyActionMenuViewModel : ViewModelBase, ISelectionSubscriber<ITherapyModel>, IItemsSubscriber
    {
        private PatientViewModel _patient;
        public PatientViewModel Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                RaisePropertyChanged();
            }
        }

        public override void Initialize(INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            CanBeActivated = true;
            Patient = navigationParameter.Parameter["Patient"] as PatientViewModel;
        }

        public bool IsVisible
        {
            get
            {
                return Items != null
                    && Items.Count > 0
                    && SelectedItem != null;
            }
        }

        public bool CanEdit()
        {
            return SelectedItem != null;
        }

        public bool CanRemoveSelection()
        {
            return SelectedItem != null;
        }

        public void RemoveSelection()
        {
            SelectedItem = null;
        }

        public void Edit()
        {
            SelectedItem.Name = "Test";
        }

        public bool CanCreate()
        {
            return Items != null;
        }

        public void Create()
        {
            Items.Add(new TherapyViewModel
            {
                Name = "Erenterale Ernährung",
                Anmerkung = "Yammie, ist das lecker"
            });

            RaisePropertyChanged(() => IsVisible);
        }

        public bool CanDelete()
        {
            return SelectedItem != null;
        }

        public void Delete()
        {
            Items.Remove(SelectedItem);
            SelectedItem = null;
            if (Items.Count == 0)
            {
                RaisePropertyChanged(() => IsVisible);
            }
        }

        private ITherapyModel _selectedItem;
        public ITherapyModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        private ObservableCollection<object> _items;

        public ObservableCollection<object> Items
        {
            get { return _items; }
            set
            {
                if (value == null)
                {
                    return;
                }

                _items = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => IsVisible);
            }
        }

        #region ISelectionSubscriber
        object ISelectionSubscriber.SelectedItem
        {
            get { return SelectedItem; }
            set { SelectedItem = (ITherapyModel)value; }
        }

        #endregion
    }
}