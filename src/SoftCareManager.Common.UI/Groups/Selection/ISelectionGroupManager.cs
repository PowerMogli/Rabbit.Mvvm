using SoftCareManager.Common.UI.Groups.Base;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    public interface ISelectionGroupManager : IBaseGroupManager<SelectionGroup, SelectionGroupSource>
    {
        SelectionGroup AddSubscriber(string selectionGroupName, DependencyObject dependencyObject);

        SelectionGroup AddPublisher(SelectionGroupSource selectionGroupSource);
    }
}