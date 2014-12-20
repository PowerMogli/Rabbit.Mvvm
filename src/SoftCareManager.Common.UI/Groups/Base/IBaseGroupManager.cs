using System.Windows;

using SoftCareManager.Contracts.Groups.Base;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public interface IBaseGroupManager<out TGroup, in TGroupSource>
        where TGroup : IGroup
        where TGroupSource : IGroupSource
    {
        void AddPublisher(TGroupSource groupSource, FrameworkElement frameworkElement);

        void AddSubscriber(string groupName, FrameworkElement frameworkElement);
    }
}