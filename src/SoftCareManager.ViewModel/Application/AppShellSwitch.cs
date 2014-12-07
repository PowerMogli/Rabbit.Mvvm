using System;
using System.Collections.Generic;

using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Application
{
    public class AppShellSwitch : ViewModelBase
    {
        private readonly List<Lazy<ViewModelBase>> _shellViewModels;

        private ViewModelBase _shellViewModel;

        public AppShellSwitch()
        {
            _shellViewModels = new List<Lazy<ViewModelBase>>
            {
                new Lazy<ViewModelBase>(() => new AppShellViewModel(0)),
                new Lazy<ViewModelBase>(() => new AppShellViewModel(1)),
                new Lazy<ViewModelBase>(() => new AppShellViewModel(2))
            };
        }

        public ViewModelBase ShellViewModel
        {
            get { return _shellViewModel; }
            private set
            {
                if (_shellViewModel != value)
                {
                    _shellViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void Switch(int shellId)
        {
            ShellViewModel = _shellViewModels[shellId].Value;
        }
    }
}