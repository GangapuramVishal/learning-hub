namespace SingletonDesignPattern
{
    /// <summary>
    /// Singleton class ensures that only one instance of ConfigurationManager is created.
    /// </summary>
    public sealed class ConfigurationManager
    {
        // Private static field to hold the single instance of the class.
        private static ConfigurationManager _instance = null;

        // Object used for locking to ensure thread-safety.
        private static readonly object _lock = new object();

        /// <summary>
        /// Private constructor to prevent direct instantiation from outside the class.
        /// </summary>
        private ConfigurationManager()
        {
            // Example of setting default configuration settings
            AppSettings = "Default Application Settings";
        }

        /// <summary>
        /// Public static method to provide a global access point to the single instance.
        /// Ensures that only one instance is created, and that it's lazily initialized.
        /// </summary>
        /// <returns>The single instance of ConfigurationManager.</returns>
        public static ConfigurationManager GetInstance()
        {
            // Double-check locking to ensure only one thread creates the instance.
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManager();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Property to simulate configuration settings.
        /// </summary>
        public string AppSettings { get; set; }

        /// <summary>
        /// Method to simulate updating the configuration settings.
        /// </summary>
        /// <param name="settings">New settings string.</param>
        public void UpdateSettings(string settings)
        {
            AppSettings = settings;
            Console.WriteLine($"Settings updated to: {AppSettings}");
        }

        /// <summary>
        /// Method to display the current settings.
        /// </summary>
        public void DisplaySettings()
        {
            Console.WriteLine($"Current Settings: {AppSettings}");
        }
    }
}
