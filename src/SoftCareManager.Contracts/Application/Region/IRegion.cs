using SoftCareManager.Contracts.ViewModel;
using System;

namespace SoftCareManager.Contracts.Application.Region
{
    public interface IRegion
    {
        string Name { get; }

        int ShellId { get; }

        void Activate(ViewModelBase viewModel);

        ViewModelBase GetContent(Type viewModelType);
    }
}