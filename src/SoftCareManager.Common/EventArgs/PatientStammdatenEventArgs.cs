using SoftCareManager.Contracts.Model.Patient;

namespace SoftCareManager.Common.EventArgs
{
    public class PatientStammdatenEventArgs : System.EventArgs
    {
        public PatientStammdatenEventArgs(IPatientModel patientModel)
        {
            PatientModel = patientModel;
        }

        public IPatientModel PatientModel { get; set; }
    }
}