using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMMRequest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new[] { "hola" };
        }

        [HttpPost]
        public ActionResult CreateRequest([FromBody] CreateRequest request)
        {
            return new OkObjectResult(request);
        }
    }
}
