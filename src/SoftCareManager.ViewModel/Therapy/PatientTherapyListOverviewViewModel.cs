using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.ViewModel.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SoftCareManager.ViewModel.Therapy
{
    public class PatientTherapyListOverviewViewModel : ViewModelBase, IPatientTherapyOverviewModel, ISelectionPublisher<ITherapyModel>, IItemsPublisher
    {
        public override void Initialize(INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            CanBeActivated = true;

            var patient = navigationParameter.Parameter["Patient"] as PatientViewModel;
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

        private ITherapyModel _selectedItem;
        public ITherapyModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ITherapyModel> _therapies;
        public ObservableCollection<ITherapyModel> Therapies
        {
            get
            { return _therapies; }
            set
            {
                _therapies = value;
                RaisePropertyChanged();
                Items = Therapies.Cast();
            }
        }

        public Guid? Id { get; set; }

        private ObservableCollection<object> _items;
        public ObservableCollection<object> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }
    }
}