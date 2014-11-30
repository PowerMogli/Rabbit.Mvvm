using SoftCareManager.Common.UI.Views.Patient;
using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Contracts.Groups.Selection;
using System.Windows;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    public class SelectionGroup : ISelectionGroup
    {
        private const string bindingPath = "SelectedItem";

        public string Name { get; set; }

        public DependencyObject SelectionSubscriber { get; set; }

        public ISelectionPublisher SelectionPublisher { get; set; }

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
                && selectionGroupSource.SelectionPublisher == null)
            {
                return;
            }

            SelectionPublisher = selectionGroupSource.SelectionPublisher;
        }

        public void Bind()
        {
            if (SelectionPublisher != null
                && SelectionSubscriber != null)
            {
                BindingOperations.SetBinding(SelectionSubscriber, BasePatientActionMenu.SelectedItemProperty, new Binding(bindingPath) { Source = SelectionPublisher, Mode = BindingMode.OneWay });
            }
        }
    }
}