﻿using System.ComponentModel.Composition;

using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;

namespace SoftCareManager.Business.WorkItems.Patient
{
    [Export(typeof (IWorkItem))]
    [Workflow(WorkItemName.PatientArticleWorkItem, WorkItemName = WorkItemName.PatientArticleWorkItem, WorkItemType = typeof (IPatientArticleWorkItem))]
    public class PatientArticleWorkItem : WorkItem<IPatientModel>, IPatientArticleWorkItem
    {
        [ImportingConstructor]
        public PatientArticleWorkItem(IAppController appWorkItem)
        {
            RootWorkItem = appWorkItem;
        }
    }
}