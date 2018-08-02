using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Araujo.MessageBroker.Publisher.API.Models.Requests;
using Araujo.MessageBroker.Publisher.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Araujo.MessageBroker.Publisher.API.Controllers
{
    [Route("api/[controller]")]    
    public class PublisherController : Controller
    {
        private readonly IPublishService Service;

        public PublisherController(IPublishService service)
        {
            this.Service = service;
        }

        
        [HttpPost]
        [Route("Publish")]
        public JsonResult Publish([FromBody]PublishRequest request)
        {
            this.Service.Publish(request.Message);
            return Json("OK");
        }


    }
}
