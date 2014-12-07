using System;

using SoftCareManager.Contracts.Model.Therapy;

namespace SoftCareManager.Business.Model.Therapy
{
    public class TherapyModel : ITherapyModel
    {
        public string Anmerkung { get; set; }

        public Guid? Id { get; set; }

        public string Name { get; set; }
    }
}