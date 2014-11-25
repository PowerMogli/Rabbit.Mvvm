using System;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface INavigationResult
    {
        bool? Result { get; }

        NavigationException Exception { get; }
    }
}