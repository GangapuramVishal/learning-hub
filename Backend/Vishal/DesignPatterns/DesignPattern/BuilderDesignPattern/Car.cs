namespace BuilderDesignPattern
{
    /// <summary>
    /// The 'Product' class that represents the complex object being built.
    /// </summary>
    public class Car
    {
        public string Engine {  get; set; }
        public string Wheels { get; set; }
        public string Body { get; set; }
        public bool HasGPS { get; set; }
        public bool HasSunroof { get; set; }

        /// <summary>
        /// Displays the constructed Car details.
        /// </summary>
        public void ShowSpecifications()
        {
            Console.WriteLine($"Engine: {Engine}");
            Console.WriteLine($"Wheels: {Wheels}");
            Console.WriteLine($"Body: {Body}"); 
            Console.WriteLine($"HasGPS: {HasGPS}");
            Console.WriteLine($"Has Sunroof: {HasSunroof}");
        }

    }
}
