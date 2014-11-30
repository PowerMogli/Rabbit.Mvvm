using SoftCareManager.Contracts.Groups.Base;
using System.Collections.Generic;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public abstract class BaseGroupManager<TGroup, TGroupSource> : IBaseGroupManager<TGroup, TGroupSource>
        where TGroup : IGroup, new()
        where TGroupSource : IGroupSource
    {
        private IDictionary<string, TGroup> _groups;

        public BaseGroupManager()
        {
            _groups = new Dictionary<string, TGroup>();
        }

        public TGroup AddSubscriber(string groupName, DependencyObject dependencyObject)
        {
            TGroup group = GetGroup(groupName);

            group = AddGroup(groupName, group);

            group.AddSubscriber(dependencyObject);

            return group;
        }

        public TGroup AddPublisher(TGroupSource groupSource)
        {
            TGroup group = GetGroup(groupSource.GroupName);

            group = AddGroup(groupSource.GroupName, group);

            group.AddPublisher(groupSource);

            return group;
        }

        private TGroup AddGroup(string groupName, TGroup group)
        {
            if (group == null)
            {
                group = new TGroup();
                group.Name = groupName;

                _groups.Add(groupName, group);
            }

            return group;
        }

        private TGroup GetGroup(string groupName)
        {
            TGroup group = default(TGroup);

            if (_groups.ContainsKey(groupName))
            {
                group = _groups[groupName];
            }

            return group;
        }
    }
}