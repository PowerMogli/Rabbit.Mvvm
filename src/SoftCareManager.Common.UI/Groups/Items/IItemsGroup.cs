using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Contracts.Groups.Base;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public interface IItemsGroup : IGroup
    {
        void AddSubscriber(DependencyObject dependencyObject);

        void AddPublisher(IGroupSource selectionGroupSource);

        void Bind();
    }
}