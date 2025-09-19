using System.ComponentModel.DataAnnotations;

namespace StudentWebAPI.Models
{
    public class StudentEntities
    {
        [Key]
        public int StdId {  get; set; }
        public string StdName { get; set; }
        public int stdAge {  get; set; }
        public string stdAddress { get; set; }

    }
}
