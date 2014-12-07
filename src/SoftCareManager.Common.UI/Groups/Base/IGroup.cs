using System.Windows;

using SoftCareManager.Contracts.Groups.Base;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public interface IGroup
    {
        string Name { get; set; }

        void AddPublisher(IGroupSource groupSource);

        void AddSubscriber(DependencyObject dependencyObject);

        void Bind();
    }
}