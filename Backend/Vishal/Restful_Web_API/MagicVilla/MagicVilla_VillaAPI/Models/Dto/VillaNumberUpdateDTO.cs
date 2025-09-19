using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNumberUpdateDTO     //Data Transfer Objects - exchanging information between different layers of an application 
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }

        public string SpecialDetails { get; set; }
    }
}
