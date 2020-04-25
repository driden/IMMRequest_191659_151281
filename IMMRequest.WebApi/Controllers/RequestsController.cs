using System.Collections.Generic;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    using Domain.Exceptions;
    using Logic.Exceptions;
    using Logic.Tests;

    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsLogic _requestsLogic;
        public RequestsController(IRequestsLogic requestsLogic)
        {
            this._requestsLogic = requestsLogic;
        }

        [HttpPost]
        public ActionResult CreateRequest([FromBody] CreateRequest request)
        {
            try
            {
                var requestId = _requestsLogic.Add(request);
                return new OkObjectResult($"request created with id {requestId}");
            }
            catch (NoSuchTopicException nste)
            {
                return BadRequest(nste.Message);
            }
            catch (InvalidAdditionalFieldForTypeException iaf)
            {
                return BadRequest(iaf.Message);
            }
            catch (NoSuchAdditionalFieldException nsaf)
            {
                return BadRequest(nsaf.Message);
            }
            catch (InvalidFieldValueCastForFieldTypeException ifve)
            {
                return BadRequest(ifve.Message);
            }
            catch (InvalidFieldRangeException nste)
            {
                return BadRequest(nste.Message);
            }
            catch (LessAdditionalFieldsThanRequiredException nste)
            {
                return BadRequest(nste.Message);
            }
        }
    }
} 
