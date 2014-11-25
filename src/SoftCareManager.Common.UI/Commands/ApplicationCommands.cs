using System.Windows.Input;

namespace SoftCareManager.Common.UI.Commands
{
    public class ApplicationCommands
    {
        public static RoutedCommand ChangePlatformCommand = new RoutedCommand();

        public static RoutedCommand ChangeShellCommand = new RoutedCommand();

        public static RoutedCommand NavigateToCommand = new RoutedCommand();
    }
}