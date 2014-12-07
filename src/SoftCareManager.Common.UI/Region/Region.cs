using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.Common.UI.Region
{
    public class Region : IRegion
    {
        private readonly ContentControl _hostControl;

        private readonly List<object> _viewContents;
        private string _name;

        public Region(string name, int shellId, ContentControl hostControl)
        {
            Name = name;
            ShellId = shellId;

            _hostControl = hostControl;
            _viewContents = new List<object>();
        }

        public string Name
        {
            get { return _name; }
            private set
            {
                if (_name != null && _name != value)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Der Name der Region darf nicht NULL sein.", _name));
                }

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Der Name der Region darf nicht leer sein.");
                }

                _name = value;
            }
        }

        public int ShellId { get; private set; }

        public void Activate(ViewModelBase viewModel)
        {
            RemoveActive(viewModel.GetType());

            SetContentOfHostControl(viewModel);

            if (viewModel.CanBeActivated
                && _viewContents.Any(content => content.GetType() == viewModel.GetType()) == false)
            {
                _viewContents.Add(viewModel);
            }
        }

        public ViewModelBase GetContent(Type viewModelType)
        {
            if (_hostControl.Content != null && _hostControl.Content.GetType() == viewModelType)
            {
                return _hostControl.Content as ViewModelBase;
            }

            return _viewContents.SingleOrDefault(content => content.GetType() == viewModelType) as ViewModelBase;
        }

        private void RemoveActive(Type viewModelType)
        {
            IRegionViewLifeTime activeContent = GetContent(viewModelType) as IRegionViewLifeTime;
            if (activeContent == null || activeContent.KeepAlive)
            {
                return;
            }

            _viewContents.Remove(activeContent);
        }

        private void SetContentOfHostControl(ViewModelBase viewModel)
        {
            // To change skin if user selected touch skin
            _hostControl.Content = null;

            _hostControl.Content = viewModel.CanBeActivated
                ? viewModel
                : null;
        }
    }
}