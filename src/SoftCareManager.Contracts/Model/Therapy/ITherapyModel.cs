namespace SoftCareManager.Contracts.Model.Therapy
{
    public interface ITherapyModel : IModel
    {
        string Anmerkung { get; set; }

        string Name { get; set; }
    }
}