using SoftCareManager.Contracts.Model.Patient;

namespace SoftCareManager.Common.EventArgs
{
    public class PatientStammdatenEventArgs : System.EventArgs
    {
        public IPatientModel PatientModel { get; set; }

        public PatientStammdatenEventArgs(IPatientModel patientModel)
        {
            PatientModel = patientModel;
        }
    }
}