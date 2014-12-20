using System.Collections.Generic;
using System.Windows;

using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Common.UI.Controls;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public abstract class BaseGroupManager<TGroup, TGroupSource> : IBaseGroupManager<TGroup, TGroupSource>
        where TGroup : class, IGroup, new()
        where TGroupSource : IGroupSource
    {
        private readonly IDictionary<string, TGroup> _groups;

        private string currentGroupKey;

        protected BaseGroupManager()
        {
            _groups = new Dictionary<string, TGroup>();
        }

        public void AddPublisher(TGroupSource groupSource, FrameworkElement frameworkElement)
        {
            Guard.ArgumentIsNotNull(frameworkElement, () => frameworkElement);
            Guard.ArgumentIsNotNull(groupSource, () => groupSource);

            frameworkElement.Loaded += OnPublisherLoaded(groupSource, frameworkElement);
        }

        public void AddSubscriber(string groupName, FrameworkElement frameworkElement)
        {
            Guard.ArgumentIsNotNull(frameworkElement, () => frameworkElement);
            Guard.ArgumentIsNotNull(groupName, () => groupName);

            frameworkElement.Loaded += OnSubscriberLoaded(groupName, frameworkElement);
        }

        private TGroup AddSubscriber(string groupName, int shellId, DependencyObject dependencyObject)
        {
            Guard.ArgumentIsNotNull(groupName, () => groupName);

            currentGroupKey = string.Join("@", groupName, shellId);

            TGroup group = GetGroup();

            group = AddGroup(groupName, group);

            group.AddSubscriber(dependencyObject);

            return group;
        }

        private TGroup AddPublisher(TGroupSource groupSource, int shellId)
        {
            Guard.ArgumentIsNotNull(groupSource, () => groupSource);
            Guard.ArgumentIsNotNull(groupSource.GroupName, () => groupSource.GroupName);

            currentGroupKey = string.Join("@", groupSource.GroupName, shellId);

            TGroup group = GetGroup();

            group = AddGroup(groupSource.GroupName, group);

            group.AddPublisher(groupSource);

            return group;
        }

        private TGroup AddGroup(string groupName, TGroup group)
        {
            if (@group != null)
            {
                return @group;
            }

            @group = new TGroup
            {
                Name = groupName,
            };

            _groups.Add(currentGroupKey, @group);

            return group;
        }

        private TGroup GetGroup()
        {
            TGroup group = default(TGroup);

            if (_groups.ContainsKey(currentGroupKey))
            {
                group = _groups[currentGroupKey];
            }

            return group;
        }

        private RoutedEventHandler OnPublisherLoaded(TGroupSource groupSource, FrameworkElement frameworkElement)
        {
            RoutedEventHandler onPublisherLoaded = null;
            onPublisherLoaded = (s, e) =>
            {
                frameworkElement.Loaded -= onPublisherLoaded;
                TGroup group = AddPublisher(groupSource, ShellIdReader.Read(frameworkElement));

                group.Bind();
            };

            return onPublisherLoaded;
        }

        private RoutedEventHandler OnSubscriberLoaded(string groupName, FrameworkElement frameworkElement)
        {
            RoutedEventHandler onSubscriberLoaded = null;
            onSubscriberLoaded = (s, e) =>
            {
                frameworkElement.Loaded -= onSubscriberLoaded;
                TGroup group = AddSubscriber(groupName, ShellIdReader.Read(frameworkElement), frameworkElement);

                group.Bind();
            };

            return onSubscriberLoaded;
        }
    }
}