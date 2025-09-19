using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Image
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsUploaded { get; set; }
        public Car Car { get; set; }
    }
}
