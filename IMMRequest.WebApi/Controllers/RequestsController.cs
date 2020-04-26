namespace IMMRequest.WebApi.Controllers
{
    using System;
    using Domain.Exceptions;
    using Logic.Exceptions;
    using Logic.Interfaces;
    using Logic.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsLogic _requestsLogic;
        public RequestsController(IRequestsLogic requestsLogic)
        {
            _requestsLogic = requestsLogic;
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
            catch (InvalidFieldRangeException ifre)
            {
                return BadRequest(ifre.Message);
            }
            catch (LessAdditionalFieldsThanRequiredException ladftre)
            {
                return BadRequest(ladftre.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
} 
