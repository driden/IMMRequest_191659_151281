namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Error;
    using Logic.Models.Request;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/requests")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsLogic _requestsLogic;

        public RequestsController(IRequestsLogic requestsLogic)
        {
            _requestsLogic = requestsLogic;
        }

        /// <summary>
        ///     Creates a new request in the system
        /// </summary>
        /// <param name="request">request body</param>
        /// <response code="201">Request created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="404">A field name could not be found</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPost]
        [DomainExceptionFilter]
        [LogicExceptionFilter]
        public ActionResult CreateRequest([FromBody] CreateRequest request)
        {
            try
            {
                var requestId = _requestsLogic.Add(request);
                return CreatedAtRoute(
                    nameof(GetOne),
                    new { Id = requestId },
                    new { Id = requestId, Text = $"request created with id {requestId}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }

        /// <summary>
        ///     Gets a list of all the status in the system
        /// </summary>
        /// <returns>A json object with a list of all requests in the system</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RequestStatusModel>), 200)]
        [AuthorizationFilter]
        public ActionResult<IEnumerable<RequestStatusModel>> GetAll()
        {
            try
            {
                return Ok(_requestsLogic.GetAllRequests());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }

        /// <summary>
        ///     Gets a request details
        /// </summary>
        /// <returns>A json object with the details of the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RequestStatusModel>), 200)]
        [Route("{id}", Name = "GetOne")]
        [LogicExceptionFilter]
        public ActionResult<RequestModel> GetOne(int id)
        {
            try
            {
                return Ok(_requestsLogic.GetRequestStatus(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }

        /// <summary>
        ///     Updates the State of a request
        /// </summary>
        /// <param name="id">The id of the request to update</param>
        /// <param name="updateStateRequest">The state to which the request should be updated to</param>
        /// <response code="200">Request updated</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="404">A request with the given Id could not be found</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPut]
        [Route("{id}")]
        [AuthorizationFilter]
        [DomainExceptionFilter]
        [LogicExceptionFilter]
        public ActionResult UpdateStatus(int id, [FromBody] UpdateStateModel updateStateRequest)
        {
            try
            {
                _requestsLogic.UpdateRequestStatus(id, updateStateRequest.NewState);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }
    }
}
