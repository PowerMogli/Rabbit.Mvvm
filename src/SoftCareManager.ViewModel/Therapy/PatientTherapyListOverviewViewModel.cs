using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.ViewModel.Patient;

namespace SoftCareManager.ViewModel.Therapy
{
    public class PatientTherapyListOverviewViewModel : ViewModelBase, IPatientTherapyOverviewModel, ISelectionPublisher<ITherapyModel>, IItemsPublisher
    {
        private ObservableCollection<object> _items;
        private ITherapyModel _selectedItem;

        private ObservableCollection<ITherapyModel> _therapies;

        public Guid? Id { get; set; }

        public ObservableCollection<object> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public ITherapyModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ITherapyModel> Therapies
        {
            get { return _therapies; }
            set
            {
                _therapies = value;
                RaisePropertyChanged();
                Items = Therapies.Cast();
            }
        }

        public override void Initialize(INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            CanBeActivated = true;

            PatientViewModel patient = navigationParameter.Parameter["Patient"] as PatientViewModel;
            if (patient != null && Therapies == null)
            {
                Therapies = new ObservableCollection<ITherapyModel>(MapToViewModel(patient.Therapies));
            }
        }

        private static List<ITherapyModel> MapToViewModel(IEnumerable<ITherapyModel> therapyModels)
        {
            List<ITherapyModel> viewModels = new List<ITherapyModel>();

            foreach (var model in therapyModels)
            {
                viewModels.Add(new TherapyViewModel
                {
                    Anmerkung = model.Anmerkung,
                    Name = model.Name
                });
            }

            return viewModels;
        }
    }
}