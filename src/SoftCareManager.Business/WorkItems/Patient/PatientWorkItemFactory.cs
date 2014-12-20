using System.ComponentModel.Composition;

using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;

namespace SoftCareManager.Business.WorkItems.Patient
{
    [Export(typeof(IPatientWorkItemFactory))]
    public class PatientWorkItemFactory : IPatientWorkItemFactory
    {
        private readonly IAppController _appController;

        private IPatientArticleWorkItem _articleWorkItem;
        public IPatientArticleWorkItem ArticleWorkItem
        {
            get { return _articleWorkItem ?? (_articleWorkItem = new PatientArticleWorkItem(_appController)); }
            set { _articleWorkItem = value; }
        }

        private IPatientHospitalWorkItem _hospitalWorkItem;
        public IPatientHospitalWorkItem HospitalWorkItem
        {
            get { return _hospitalWorkItem ?? (_hospitalWorkItem = new PatientHospitalWorkItem(_appController)); }
            set { _hospitalWorkItem = value; }
        }

        private IPatientInsuranceWorkItem _insuranceWorkItem;
        public IPatientInsuranceWorkItem InsuranceWorkItem
        {
            get { return _insuranceWorkItem ?? (_insuranceWorkItem = new PatientInsuranceWorkItem(_appController)); }
            set { _insuranceWorkItem = value; }
        }

        private IPatientTherapyWorkItem _therapyWorkItem;
        public IPatientTherapyWorkItem TherapyWorkItem
        {
            get { return _therapyWorkItem ?? (_therapyWorkItem = new PatientTherapyWorkItem()); }
            set { _therapyWorkItem = value; }
        }

        public PatientWorkItemFactory(IAppController appController)
        {
            _appController = appController;
        }
    }
}