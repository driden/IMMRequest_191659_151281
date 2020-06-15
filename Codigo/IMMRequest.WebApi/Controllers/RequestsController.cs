namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Domain.Exceptions;
    using Filters;
    using Logic.Exceptions;
    using Logic.Exceptions.CreateTopic;
    using Logic.Interfaces;
    using Logic.Models.Error;
    using Logic.Models.Request;
    using Logic.Models.State;
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
        /// <param name="requestModel">request body</param>
        /// <response code="201">Request created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="404">A field name could not be found</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPost]
        public ActionResult CreateRequest([FromBody] CreateRequestModel requestModel)
        {
            try
            {
                var requestId = _requestsLogic.Add(requestModel);
                return CreatedAtRoute(
                    nameof(GetOne),
                    new { Id = requestId },
                    new { Id = requestId, Text = $"request created with id {requestId}" });
            }
            catch (NoSuchTypeException nste)
            {
                return BadRequest(new ErrorModel(nste.Message));
            }
            catch (InvalidAdditionalFieldForTypeException iaf)
            {
                return BadRequest(new ErrorModel(iaf.Message));
            }
            catch (NoSuchAdditionalFieldException nsaf)
            {
                return NotFound(new ErrorModel(nsaf.Message));
            }
            catch (InvalidFieldValueCastForFieldTypeException ifve)
            {
                return BadRequest(new ErrorModel(ifve.Message));
            }
            catch (InvalidFieldRangeException ifre)
            {
                return BadRequest(new ErrorModel(ifre.Message));
            }
            catch (LessAdditionalFieldsThanRequiredException ladftre)
            {
                return BadRequest(new ErrorModel(ladftre.Message));
            }
            catch (InvalidDetailsException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
            catch (InvalidNameFormatException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
            catch (InvalidEmailException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
            catch (InvalidPhoneNumberException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
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
        public ActionResult<RequestModel> GetOne(int id)
        {
            try
            {
                return Ok(_requestsLogic.GetRequestStatus(id));
            }
            catch (InvalidTopicIdException invalidRequestId)
            {
                return BadRequest(new ErrorModel(invalidRequestId.Message));
            }
            catch (NoSuchRequestException noSuchRequest)
            {
                return BadRequest(new ErrorModel(noSuchRequest.Message));
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
        public ActionResult UpdateStatus(int id, [FromBody] UpdateStateModel updateStateRequest)
        {
            try
            {
                _requestsLogic.UpdateRequestStatus(id, updateStateRequest.NewState);
                return NoContent();
            }
            catch (InvalidStateNameException ex)
            {
                return BadRequest(new ErrorModel(ex.Message));
            }
            catch (InvalidTopicIdException ex)
            {
                return NotFound(new ErrorModel(ex.Message));
            }
            catch (NoSuchRequestException ex)
            {
                return NotFound(new ErrorModel(ex.Message));
            }
            catch (InvalidStateException ex)
            {
                return BadRequest(new ErrorModel(ex.Message));
            }
            catch (InvalidRequestIdException ex)
            {
                return BadRequest(new ErrorModel(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }
        
        /// <summary>
        /// Gets all request by a user
        /// </summary>
        /// <returns>A json object with the details of the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StateReportModel>), 200)]
        [Route("{mail}")]
        [AuthorizationFilter]
        public ObjectResult GetAllRequestByMail(string mail)
        {
            try
            {
                return Ok(_requestsLogic.GetRequestByMail(mail));
            }
            catch (NoSuchRequestException noSuchRequest)
            {
                return BadRequest(new ErrorModel(noSuchRequest.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }
    }
}
