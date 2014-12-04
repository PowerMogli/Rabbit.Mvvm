﻿using System.Collections.Generic;
using System.Windows;
using SoftCareManager.Contracts.Groups.Base;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public abstract class BaseGroupManager<TGroup, TGroupSource> : IBaseGroupManager<TGroup, TGroupSource>
        where TGroup : class, IGroup, new()
        where TGroupSource : IGroupSource
    {
        private readonly IDictionary<string, TGroup> _groups;

        protected BaseGroupManager()
        {
            _groups = new Dictionary<string, TGroup>();
        }

        public TGroup AddSubscriber(string groupName, DependencyObject dependencyObject)
        {
            var group = GetGroup(groupName);

            group = AddGroup(groupName, group);

            group.AddSubscriber(dependencyObject);

            return group;
        }

        public TGroup AddPublisher(TGroupSource groupSource)
        {
            var group = GetGroup(groupSource.GroupName);

            group = AddGroup(groupSource.GroupName, group);

            group.AddPublisher(groupSource);

            return group;
        }

        private TGroup AddGroup(string groupName, TGroup group)
        {
            if (@group != null) return @group;

            @group = new TGroup { Name = groupName };

            _groups.Add(groupName, @group);

            return group;
        }

        private TGroup GetGroup(string groupName)
        {
            var group = default(TGroup);

            if (_groups.ContainsKey(groupName))
            {
                group = _groups[groupName];
            }

            return group;
        }
    }
}