namespace SoftCareManager.Contracts.Model.Therapy
{
    public interface ITherapyModel : IModel
    {
        string Name { get; set; }

        string Anmerkung { get; set; }
    }
}