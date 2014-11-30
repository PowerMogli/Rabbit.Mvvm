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

        public ISelectionPublisher SelectionPublisher { get; private set; }

        public string GroupName { get; private set; }
    }
}