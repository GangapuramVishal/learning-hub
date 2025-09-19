
using Di_LifeTime.Interfaces;

namespace Di_LifeTime.Services
{
        public class Scoped : IScoped
        {
            private readonly Guid number;
            public Scoped()
            {
                number = Guid.NewGuid();
            }
            public string PrintGuidNumber()
            {
                return number.ToString();
            }
        }
}
