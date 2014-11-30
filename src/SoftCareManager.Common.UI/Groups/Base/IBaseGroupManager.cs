using SoftCareManager.Contracts.Groups.Base;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public interface IBaseGroupManager<TGroup, TGroupSource>
        where TGroup : IGroup
        where TGroupSource : IGroupSource
    {
        TGroup AddSubscriber(string groupName, DependencyObject dependencyObject);

        TGroup AddPublisher(TGroupSource groupSource);
    }
}