using System.Windows;

using SoftCareManager.Contracts.Groups.Base;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public interface IBaseGroupManager<out TGroup, in TGroupSource>
        where TGroup : IGroup
        where TGroupSource : IGroupSource
    {
        TGroup AddPublisher(TGroupSource groupSource);

        TGroup AddSubscriber(string groupName, DependencyObject dependencyObject);
    }
}