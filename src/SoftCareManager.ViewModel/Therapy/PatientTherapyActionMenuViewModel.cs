using System.Collections.ObjectModel;

using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.ViewModel.Patient;

namespace SoftCareManager.ViewModel.Therapy
{
    public class PatientTherapyActionMenuViewModel : ViewModelBase, ISelectionSubscriber<ITherapyModel>, IItemsSubscriber
    {
        private ObservableCollection<object> _items;
        private PatientViewModel _patient;
        private ITherapyModel _selectedItem;

        public bool IsVisible
        {
            get
            {
                return Items != null
                       && Items.Count > 0
                       && SelectedItem != null;
            }
        }

        public PatientViewModel Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                RaisePropertyChanged();
            }
        }

        #region IItemsSubscriber Members

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

        #endregion

        #region ISelectionSubscriber<ITherapyModel> Members

        public ITherapyModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        #endregion

        #region ISelectionSubscriber

        object ISelectionSubscriber.SelectedItem
        {
            get { return SelectedItem; }
            set { SelectedItem = (ITherapyModel)value; }
        }

        #endregion

        public override void Initialize(INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            CanBeActivated = true;
            Patient = navigationParameter.Parameter["Patient"] as PatientViewModel;
        }

        public bool CanCreate()
        {
            return Items != null;
        }

        public bool CanDelete()
        {
            return SelectedItem != null;
        }

        public bool CanEdit()
        {
            return SelectedItem != null;
        }

        public bool CanRemoveSelection()
        {
            return SelectedItem != null;
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

        public void Delete()
        {
            Items.Remove(SelectedItem);
            SelectedItem = null;
            if (Items.Count == 0)
            {
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public void Edit()
        {
            SelectedItem.Name = "Test";
        }

        public void RemoveSelection()
        {
            SelectedItem = null;
        }
    }
}