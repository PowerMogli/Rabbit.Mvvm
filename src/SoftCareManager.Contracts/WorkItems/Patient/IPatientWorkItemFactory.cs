namespace SoftCareManager.Contracts.WorkItems.Patient
{
    public interface IPatientWorkItemFactory
    {
        IPatientArticleWorkItem ArticleWorkItem { get; set; }

        IPatientHospitalWorkItem HospitalWorkItem { get; set; }

        IPatientInsuranceWorkItem InsuranceWorkItem { get; set; }

        IPatientTherapyWorkItem TherapyWorkItem { get; set; }
    }
}