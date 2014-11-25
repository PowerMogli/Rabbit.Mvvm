using System;
using System.ComponentModel.Composition;

namespace SoftCareManager.Contracts.WorkItems
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class WorkflowAttribute : ExportAttribute
    {
        public WorkflowAttribute()
            : this(string.Empty) { }

        public WorkflowAttribute(string contractName)
            : base(contractName, typeof(IWorkItem))
        {

        }

        public Type WorkItemType { get; set; }

        public string WorkItemName { get; set; }
    }
}