namespace IMMRequest.WebApi.Controllers
{
    using System.Collections.Generic;
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Request;
    using Logic.Models.State;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/requests")]
    [ApiController]
    [SystemExceptionFilter]
    [DomainExceptionFilter]
    [LogicExceptionFilter]
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
        /// <param name="requestModel">request body</param>
        /// <response code="201">Request created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="404">A field name could not be found</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPost]
        public ActionResult CreateRequest([FromBody] CreateRequestModel requestModel)
        {
            var requestId = _requestsLogic.Add(requestModel);
            return CreatedAtRoute(
                nameof(GetOne),
                new { Id = requestId },
                new { Id = requestId, Text = $"request created with id {requestId}" });
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
            return Ok(_requestsLogic.GetAllRequests());
        }

        /// <summary>
        ///     Gets a request details
        /// </summary>
        /// <returns>A json object with the details of the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RequestStatusModel>), 200)]
        [Route("{id}", Name = "GetOne")]
        public ActionResult<RequestModel> GetOne(int id)
        {
            return Ok(_requestsLogic.GetRequestStatus(id));
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
        public ActionResult UpdateStatus(int id, [FromBody] UpdateStateModel updateStateRequest)
        {
            _requestsLogic.UpdateRequestStatus(id, updateStateRequest.NewState);
            return NoContent();
        }

        /// <summary>
        ///     Gets all request by a user mail between two dates
        /// </summary>
        /// <param name="mail"> The mail from user </param>
        /// <returns>A json object with the details of the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StateReportModel>), 200)]
        [Route("{mail}")]
        [AuthorizationFilter]
        public ActionResult<IEnumerable<StateReportModel>>  GetAllRequestByMail(string mail)
        { 
            return Ok(_requestsLogic.GetRequestByMail(mail)); 
        }
    }
}
