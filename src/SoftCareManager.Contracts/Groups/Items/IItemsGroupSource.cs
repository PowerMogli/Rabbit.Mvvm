using SoftCareManager.Contracts.Groups.Base;

namespace SoftCareManager.Contracts.Groups.Items
{
    public interface IItemsGroupSource : IGroupSource
    {
        IItemsPublisher ItemsPublisher { get; }
    }
}