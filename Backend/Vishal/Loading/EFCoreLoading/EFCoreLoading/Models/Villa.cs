using System.ComponentModel.DataAnnotations;

namespace EFCoreLoading.Models
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        //navigation property
        public ICollection<VillaAmenity> VillaAmenity { get; set; }
    }
}
