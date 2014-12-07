using System.Windows.Input;

namespace SoftCareManager.Common.UI.Commands
{
    public class PatientCommands
    {
        /// <summary>
        /// The create command
        /// </summary>
        public static RoutedCommand CreateCommand = new RoutedCommand();

        /// <summary>
        /// The edit command
        /// </summary>
        public static RoutedCommand EditCommand = new RoutedCommand();
    }
}