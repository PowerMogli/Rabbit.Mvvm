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

        public DependencyObject SelectionSubscriber { get; set; }

        public ISelectionPublisher SelectionPublisher { get; set; }

        public void Bind()
        {
            if (SelectionPublisher == null
                || SelectionSubscriber == null)
            {
                return;
            }

            BindingOperations.SetBinding(SelectionSubscriber, BaseActionMenu2.SelectedItemProperty, new Binding(BindingPath)
            {
                Source = SelectionPublisher,
                Mode = BindingMode.OneWay
            });
        }

        public void AddSubscriber(DependencyObject dependencyObject)
        {
            ISelectionSubscriber selectionSubscriber = dependencyObject as ISelectionSubscriber;
            if (selectionSubscriber != null)
            {
                SelectionSubscriber = dependencyObject;
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

            SelectionPublisher = selectionGroupSource.SelectionPublisher;
        }
    }
}