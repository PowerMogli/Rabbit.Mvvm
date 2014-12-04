using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Common.UI.Views.Patient;
using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Contracts.Groups.Selection;
using System.Windows;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    public class SelectionGroup : IGroup
    {
        private const string BindingPath = "SelectedItem";

        public string Name { get; set; }

        private DependencyObject _selectionSubscriber;

        private ISelectionPublisher _selectionPublisher;

        public void Bind()
        {
            if (_selectionPublisher == null
                || _selectionSubscriber == null)
            {
                return;
            }

            BindingOperations.SetBinding(_selectionSubscriber, BaseActionMenu2.SelectedItemProperty, new Binding(BindingPath)
            {
                Source = _selectionPublisher,
                Mode = BindingMode.OneWay
            });
        }

        public void AddSubscriber(DependencyObject dependencyObject)
        {
            ISelectionSubscriber subscriber = dependencyObject as ISelectionSubscriber;
            if (subscriber != null)
            {
                _selectionSubscriber = dependencyObject;
            }
        }

        public void AddPublisher(IGroupSource groupSource)
        {
            var selectionGroupSource = groupSource as ISelectionGroupSource;
            if (selectionGroupSource == null
                || selectionGroupSource.SelectionPublisher == null)
            {
                return;
            }

            _selectionPublisher = selectionGroupSource.SelectionPublisher;
        }
    }
}
