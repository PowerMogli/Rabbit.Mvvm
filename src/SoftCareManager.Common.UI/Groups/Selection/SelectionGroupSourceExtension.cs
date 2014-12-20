using System;
using System.Windows.Markup;

using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Contracts.Groups.Selection;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    [MarkupExtensionReturnType(typeof (SelectionGroupSource))]
    public class SelectionGroupSourceExtension : BaseGroupSourceExtension<SelectionGroupSource>
    {
        public SelectionGroupSourceExtension()
            : this(string.Empty)
        {
        }

        public SelectionGroupSourceExtension(string groupName)
            : base(groupName)
        {
            GroupSource = new Lazy<SelectionGroupSource>(() => new SelectionGroupSource(GroupName, DataContext as ISelectionPublisher));
        }

        protected override void OnDataContextFound()
        {
            SelectionGroupManager.SetSelectionGroupSource(TargetObject, GroupSource.Value);
        }
    }
}