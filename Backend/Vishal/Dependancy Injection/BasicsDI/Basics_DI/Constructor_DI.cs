using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics_DI
{
    public class Constructor_DI
    {
        /*Dependencies are provided through a class's constructor. When an instance of the class is created, the necessary
         *dependencies are passed in as parameters to the constructor. Constructor injection promotes the principle of "tell,
         *don't ask" and ensures that a class's dependencies are clearly defined at instantiation time.
         */
    }

    public interface IEvent
    {
        public void LoadEventDetails();
    }
    public class SportsEvent : IEvent
    {
        public void LoadEventDetails()
        {
            Console.WriteLine("This is a sports event class");
        }
    }
    public class CulturalEvent : IEvent
    {
        public void LoadEventDetails()
        {
            Console.WriteLine("This is a Cultural event class");
        }
    }

    public class TechEvent : IEvent
    {
        public void LoadEventDetails()
        {
            Console.WriteLine("This is an Technology event class");
        }
    }
    public class PartyEvent : IEvent
    {
        public void LoadEventDetails()
        {
            Console.WriteLine("This is an Party event class");
        }
    }

    public class College
    {
        private IEvent _events; //private field of type IEvent
        //constructor
        public College(IEvent events) //a constructor that accepts an object of type IEvent. This constructor injects
                                      //the dependency of the event into the College class.
        {
            _events = events;
        }
        public void GetEvents()
        {
            this._events.LoadEventDetails();
        }
    }

}
