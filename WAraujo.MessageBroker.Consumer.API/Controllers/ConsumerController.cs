using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Araujo.MessageBroker.Consumer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WAraujo.MessageBroker.Consumer.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Consumer")]
    public class ConsumerController : Controller
    {
        IConsumerService Service;

        public ConsumerController(IConsumerService service)
        {
            this.Service = service;
        }

        // GET: api/Consumer
        [HttpGet]
        public JsonResult Get()
        {
            var result = Service.ListMessages();

            return Json(result);
        } 
    }
}
