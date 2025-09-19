using Di_LifeTime.Interfaces;

namespace Di_LifeTime.Services
{
    public class Transient: ITransient
    {
            private readonly Guid number;
            public Transient()
            {
                number = Guid.NewGuid();
            }
            public string PrintGuidNumber()
            {
                return number.ToString();
            }
    }
}
