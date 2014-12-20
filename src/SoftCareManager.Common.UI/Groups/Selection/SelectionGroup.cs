using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Common.UI.Views.Patient;
using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Contracts.Groups.Selection;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    public class SelectionGroup : IGroup
    {
        private readonly List<DependencyObject> _selectionSubscriber;
        private ISelectionPublisher _selectionPublisher;

        public SelectionGroup()
        {
            _selectionSubscriber = new List<DependencyObject>();
        }

        public string Name { get; set; }

        public void AddPublisher(IGroupSource groupSource)
        {
            ISelectionGroupSource selectionGroupSource = groupSource as ISelectionGroupSource;
            if (selectionGroupSource == null
                || selectionGroupSource.SelectionPublisher == null)
            {
                return;
            }

            _selectionPublisher = selectionGroupSource.SelectionPublisher;
        }

        public void AddSubscriber(DependencyObject dependencyObject)
        {
            ISelectionSubscriber subscriber = dependencyObject as ISelectionSubscriber;
            if (subscriber != null)
            {
                _selectionSubscriber.Add(dependencyObject);
            }
        }

        public void Bind()
        {
            if (_selectionPublisher == null
                || _selectionSubscriber == null)
            {
                return;
            }

            for (var index = _selectionSubscriber.Count - 1; index >= 0; index--)
            {
                BindingOperations.ClearBinding(_selectionSubscriber[index], BaseActionMenu2.SelectedItemProperty);
                BindingOperations.SetBinding(_selectionSubscriber[index], BaseActionMenu2.SelectedItemProperty, new Binding("SelectedItem")
                {
                    Source = _selectionPublisher,
                    Mode = BindingMode.OneWay
                });

                _selectionSubscriber.RemoveAt(index);
            }
        }
    }
}