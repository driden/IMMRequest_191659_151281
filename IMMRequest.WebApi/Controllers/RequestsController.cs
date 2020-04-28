namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
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

        /// <summary>
        /// Creates a new request in the system 
        /// </summary>
        /// <param name="request">request body</param>
        /// <response code="200">Request created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="404">A field name could not be found</response>
        /// <response code="500">Something is wrong with the server</response>
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
                return NotFound(nsaf.Message);
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

        /// <summary>
        /// Gets a list of all the status in the system
        /// </summary>
        /// <returns>A json object with a list of all requests in the system</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAllRequestsStatusResponse>), 200)]
        public ObjectResult GetAll()
        {
            try
            {
                return new ObjectResult(_requestsLogic.GetAllRequests());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the State of a request
        /// </summary>
        /// <param name="id">The id of the request to update</param>
        /// <param name="updateStateRequest">The state to which the request should be updated to</param>
        /// <response code="200">Request updated</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="404">A request with the given Id could not be found</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateStatus(int id, [FromBody]UpdateStateRequest updateStateRequest)
        {
            try
            {
                _requestsLogic.UpdateRequestStatus(id, updateStateRequest.NewState);
                return new OkResult();
            }
            catch (InvalidStateNameException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
            catch (InvalidRequestIdException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
            catch (NoSuchRequestException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
            catch (InvalidStateException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(ex.Message));
            }
        }
    }
}
