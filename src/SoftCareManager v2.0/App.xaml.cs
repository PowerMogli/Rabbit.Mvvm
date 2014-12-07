using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

using SoftCareManager.Views.Application;

namespace SoftCareManager
{
    public partial class App : Application, IPartImportsSatisfiedNotification
    {
        [Export] private CompositionContainer container;
        [Import(typeof (AppShellContainerView))] private AppShellContainerView shellContainerView;

        public void OnImportsSatisfied()
        {
            shellContainerView.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeDependencyInjection();
        }

        private void InitializeDependencyInjection()
        {
            container = new CompositionContainer(new DirectoryCatalog("..\\..\\..\\DLLs"));
            container.ComposeParts(this);
        }
    }
}