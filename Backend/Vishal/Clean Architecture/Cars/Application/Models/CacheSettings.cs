using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CacheSettings
    {
        public int SlidingExpiration {  get; set; }   //From creation of cache till how much time should cache last
        public string DistinationUrl { get; set; }
        public string ApplicationName { get; set; }
        public bool BypassCache { get; set; }  //if don't want to consider a cache
    }
}
