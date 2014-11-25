namespace SoftCareManager.Contracts.General
{
    /// <summary>
    /// Contains commonly used event topics.
    /// </summary>
    public static class EventTopics
    {
        public const string AppControllerInitialized = "topic://AppController.Initialized";

        public const string NavigationToPatientStammdaten = "topic://Patient.Navigation.Stammdaten.";
    }
}