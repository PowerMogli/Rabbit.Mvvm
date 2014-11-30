using SoftCareManager.Contracts.Groups.Base;

namespace SoftCareManager.Contracts.Groups.Selection
{
    public interface ISelectionGroupSource : IGroupSource
    {
        ISelectionPublisher SelectionPublisher { get; }
    }
}