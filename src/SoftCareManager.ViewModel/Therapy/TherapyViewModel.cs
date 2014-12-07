using System;

using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Therapy
{
    internal class TherapyViewModel : ViewModelBase, ITherapyModel
    {
        private string _anmerkung;
        private string _name;

        public string Anmerkung
        {
            get { return _anmerkung; }
            set
            {
                _anmerkung = value;
                RaisePropertyChanged();
            }
        }

        public Guid? Id { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
    }
}