using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics_DI
{
    internal class PropertyInjection_Di
    {
        /*Recommended using when a class has optional dependencies, or where the implementations may need to be swapped.
         * Different logger implementations could be used in this way.Does not require the creation of a new object
         * or modifying the existing one. Without changing the object state, it could work.
         */
    }
    public interface IService
    {
        void Serve();
    }
    public class Service1 : IService
    {
        public void Serve()
        {
            Console.WriteLine("Service1 Called");
        }
    }
    public class Service2 : IService
    {
        public void Serve()
        {
            Console.WriteLine("Service2 called");
        }
    }
    public class Client
    {
        private IService _service;  //private field service of type IServece which will hold a reference to an object that implements the IService interface.
        public IService Service     // property named Service with a setter that allows setting the _service field with an instance of a class implementing IService.
        {
            set { this._service = value; }
        }
        public void ServeMethod()
        {
            this._service.Serve();
        }
    }
}
