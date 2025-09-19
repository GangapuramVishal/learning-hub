namespace Application.Models
{
    //when new car is created we expect below info from user
    public class NewCar
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public decimal Mileage { get; set; }
        public string Transmission { get; set; }
        public int Seats { get; set; }
        public string FuelType { get; set; }
        public string EngineSize { get; set; }
        public bool HasSunroof { get; set; }
    }
}
