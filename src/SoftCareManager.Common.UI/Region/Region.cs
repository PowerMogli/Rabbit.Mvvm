using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Region
{
    public class Region : IRegion
    {
        private string name;

        private ContentControl hostControl;

        List<object> viewContents;

        public Region(string name, int shellId, ContentControl hostControl)
        {
            Name = name;
            ShellId = shellId;

            this.hostControl = hostControl;
            viewContents = new List<object>();
        }

        public int ShellId { get; private set; }

        public string Name
        {
            get { return name; }
            private set
            {
                if (this.name != null && this.name != value)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Der Name der Region darf nicht NULL sein.", this.name));
                }

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Der Name der Region darf nicht leer sein.");
                }

                this.name = value;
            }
        }

        public void Activate(ViewModelBase viewModel)
        {
            RemoveActive(viewModel.GetType());

            SetContentOfHostControl(viewModel);

            if (viewModel.CanBeActivated
                && viewContents.Any(content => content.GetType() == viewModel.GetType()) == false)
            {
                viewContents.Add(viewModel);
            }
        }

        private void SetContentOfHostControl(ViewModelBase viewModel)
        {
            // To change skin if user selected touch skin
            hostControl.Content = null;

            hostControl.Content = viewModel.CanBeActivated ? viewModel : null;
        }

        private void RemoveActive(Type viewModelType)
        {
            var activeContent = GetContent(viewModelType) as IRegionViewLifeTime;
            if (activeContent == null || activeContent.KeepAlive)
            {
                return;
            }

            viewContents.Remove(activeContent);
        }

        public ViewModelBase GetContent(Type viewModelType)
        {
            if (hostControl.Content != null && hostControl.Content.GetType() == viewModelType)
            {
                return hostControl.Content as ViewModelBase;
            }

            return viewContents.SingleOrDefault(content => content.GetType() == viewModelType) as ViewModelBase;
        }
    }
}