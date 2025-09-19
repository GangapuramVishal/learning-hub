using System.ComponentModel.DataAnnotations;

namespace TaskManagmentSystemAPI.Models.Dto
{
    public class TaskUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
