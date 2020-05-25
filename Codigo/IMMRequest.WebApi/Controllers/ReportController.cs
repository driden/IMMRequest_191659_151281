using System;
using System.Collections.Generic;
using IMMRequest.Logic.Exceptions;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportsLogic _reportsLogic;

        public ReportController(IReportsLogic reportsLogic)
        {
            _reportsLogic = reportsLogic;
        }

        /// <summary>
        /// Gets all request by a user
        /// </summary>
        /// <returns>A json object with the details of the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAllRequestsByMail>), 200)]
        [Route("{id}")]
        public ObjectResult GetAllRequestByMail(string mail)
        {
            try
            {
                return new ObjectResult(_reportsLogic.ReportsRequestByMail(mail));
            }
            catch (NoSuchRequestException noSuchRequest)
            {
                return BadRequest(new ErrorResponse(noSuchRequest.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(ex.Message));
            }
        }

    }
}
