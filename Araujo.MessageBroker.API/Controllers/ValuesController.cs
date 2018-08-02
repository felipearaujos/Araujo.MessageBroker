using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Araujo.MessageBroker.Publisher.Service;
using Microsoft.AspNetCore.Mvc;

namespace Araujo.MessageBroker.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IPublishService Service;

        public ValuesController(IPublishService service)
        {
            this.Service = service;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value message";
        }

        // GET api/values/5
        //[HttpGet]
        //[Route("Publish/{message}")]
        //public JsonResult Publish(string message)
        //{
        //    this.Service.Publish(message);
        //    return Json("OK");
        //}

        [HttpPost]
        [Route("Publish")]
        public JsonResult Publish(string message)
        {
            this.Service.Publish(message);
            return Json("OK");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
