using Di_LifeTime.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Di_LifeTime.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ISingleton _singleton1;
        private readonly ISingleton _singleton2;
        private readonly ITransient _transient1;
        private readonly ITransient _transient2;            
        private readonly IScoped _scoped1;
        private readonly IScoped _scoped2;
        public DataController(ISingleton singleton1, ISingleton singleton2, ITransient transient1,ITransient transient2, IScoped scoped1, IScoped scoped2)
        {
            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
        }
        // GET: api/<DateController>
        [HttpGet]
        public DataResponse Get()
        {
            var response = new DataResponse();
            response.Singleton1 = _singleton1.PrintGuidNumber();
            response.Singleton2 = _singleton1.PrintGuidNumber();
            response.Transient1 = _transient1.PrintGuidNumber();
            response.Transient2 = _transient2.PrintGuidNumber();
            response.Scoped1 = _scoped1.PrintGuidNumber();
            response.Scoped2 = _scoped2.PrintGuidNumber();
            return response;
        }

            // GET api/<DateController>/5
            [HttpGet("{id}")]
            public string Get(int id)
            {
                return "value";
            }

            // POST api/<DateController>
            [HttpPost]
            public void Post([FromBody] string value)
            {
            }

            // PUT api/<DateController>/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE api/<DateController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }

    }
}
