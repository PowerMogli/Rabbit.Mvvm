using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Contracts.Groups.Selection;
using System;
using System.Windows.Markup;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    [MarkupExtensionReturnType(typeof(SelectionGroupSource))]
    public class SelectionGroupSourceExtension : BaseGroupSourceExtension<SelectionGroupSource>
    {
        public SelectionGroupSourceExtension()
        {
            _groupSource = new Lazy<SelectionGroupSource>(() => new SelectionGroupSource(GroupName, _dataContext as ISelectionPublisher));
        }

        public SelectionGroupSourceExtension(string groupName)
            : this()
        {
            GroupName = groupName;
        }

        protected override void OnDataContextFound()
        {
            SelectionGroupManager.SetSelectionGroupSource(_targetObject, _groupSource.Value);
        }
    }
}