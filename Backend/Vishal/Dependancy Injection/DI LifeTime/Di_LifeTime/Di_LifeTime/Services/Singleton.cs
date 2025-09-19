using Di_LifeTime.Interfaces;

namespace Di_LifeTime.Services
{
    public class Singleton : ISingleton
    {
        private readonly Guid number;
        public Singleton()
        {
            number = Guid.NewGuid();
        }
        public string PrintGuidNumber()
        {
            return number.ToString();
        }
    }
}
