using SoftCareManager.Contracts.Groups.Base;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public interface IGroup
    {
        string Name { get; set; }

        void Bind();

        void AddSubscriber(DependencyObject dependencyObject);

        void AddPublisher(IGroupSource groupSource);
    }
}