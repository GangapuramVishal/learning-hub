namespace Application.Models
{
    public class ImageDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsUploaded { get; set; }
        public CarDto Car { get; set; }
    }
}
