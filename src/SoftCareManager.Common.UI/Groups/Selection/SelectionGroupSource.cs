using SoftCareManager.Contracts.Groups.Selection;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    public class SelectionGroupSource : ISelectionGroupSource
    {
        public SelectionGroupSource(string selectionGroupName, ISelectionPublisher selectionPublisher)
        {
            GroupName = selectionGroupName;
            SelectionPublisher = selectionPublisher;
        }

        public string GroupName { get; private set; }

        public ISelectionPublisher SelectionPublisher { get; private set; }
    }
}