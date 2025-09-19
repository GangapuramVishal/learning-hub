using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Segregation_Principle
{
    public interface IVehicleViolating
    {
        public void Start();
        public void Drive();
        public void Fly();
    }
    
}
