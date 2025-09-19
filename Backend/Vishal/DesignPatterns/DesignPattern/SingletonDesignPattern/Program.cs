namespace SingletonDesignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Get the Singleton instance and display the default settings.
            ConfigurationManager configManager = ConfigurationManager.GetInstance();
            configManager.DisplaySettings();

            // Update settings using the Singleton instance.
            configManager.UpdateSettings("Custom Application Settings");

            // Retrieve the instance again and display updated settings.
            ConfigurationManager anotherConfigManager = ConfigurationManager.GetInstance();
            anotherConfigManager.DisplaySettings();

            // Check if both instances are the same.
            bool isSameInstance = ReferenceEquals(configManager, anotherConfigManager);
            Console.WriteLine($"Are both instances the same? {isSameInstance}");
        }
    }
}
