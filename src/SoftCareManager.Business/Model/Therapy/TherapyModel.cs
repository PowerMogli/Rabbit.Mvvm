using SoftCareManager.Contracts.Model.Therapy;

namespace SoftCareManager.Business.Model.Therapy
{
    public class TherapyModel : ITherapyModel
    {
        public string Name { get; set; }

        public string Anmerkung { get; set; }

        public System.Guid? Id { get; set; }
    }
}