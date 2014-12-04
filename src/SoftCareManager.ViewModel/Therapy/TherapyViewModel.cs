using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Therapy
{
    class TherapyViewModel : ViewModelBase, ITherapyModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _anmerkung;
        public string Anmerkung
        {
            get { return _anmerkung; }
            set
            {
                _anmerkung = value;
                RaisePropertyChanged();
            }
        }

        public System.Guid? Id { get; set; }
    }
}