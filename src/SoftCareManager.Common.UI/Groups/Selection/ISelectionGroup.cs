using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Contracts.Groups.Selection;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    public interface ISelectionGroup : IGroup
    {
        DependencyObject SelectionSubscriber { get; set; }

        ISelectionPublisher SelectionPublisher { get; set; }

        void AddSubscriber(DependencyObject dependencyObject);

        void AddPublisher(IGroupSource dependencyObject);

        void Bind();
    }
}