using SoftCareManager.Views.Application;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace SoftCareManager
{
    public partial class App : Application, IPartImportsSatisfiedNotification
    {
        [Import(typeof(AppShellContainerView))]
        private AppShellContainerView shellContainerView;

        [Export]
        private CompositionContainer container;

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

        public void OnImportsSatisfied()
        {
            shellContainerView.Show();
        }
    }
}