using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsLogic _requestsLogic;
        public RequestsController(IRequestsLogic requestsLogic)
        {
            this._requestsLogic = requestsLogic;
        }
        
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new[] { "hola" };
        }

        [HttpPost]
        public ActionResult CreateRequest([FromBody] CreateRequest request)
        {
            _requestsLogic.Add(request);
            return new OkObjectResult("Ok");
        }
    }
}
