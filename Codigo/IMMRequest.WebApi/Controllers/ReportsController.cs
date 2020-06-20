namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Filters;
    using IMMRequest.Logic.Models.Type;
    using Logic.Interfaces;
    using Logic.Models.State;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/reports")]
    [ApiController]
    [SystemExceptionFilter]
    [DomainExceptionFilter]
    [LogicExceptionFilter]
    public class ReportsController: ControllerBase
    {
        private readonly IReportsLogic _reportsLogic;

        public ReportsController(IReportsLogic reportsLogic)
        {
            _reportsLogic = reportsLogic;
        }

        /// <summary>
        ///     Gets all request by a user mail between two dates
        /// </summary>
        /// <param name="reportInput"> Input to generate report a </param>
        /// <returns>A json object with the details of the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StateReportModel>), 200)]
        [Route("a")]
        [AuthorizationFilter]
        public ActionResult<IEnumerable<StateReportModel>> GetAllRequestByMail([FromBody] SearchByMailModel reportInput)
        { 
            return Ok(_reportsLogic.GetRequestByMail(reportInput.Mail, reportInput.StartDate, reportInput.EndDate)); 
        }
        
        /// <summary>
        ///     Lists all the most used type for requests
        /// </summary>
        /// <returns>Returns the list of types in a topic</returns>
        [HttpGet]
        [Route("b")]
        [AuthorizationFilter]
        public ActionResult<IEnumerable<TypeReportModel>> GetMostUsedTypes([FromBody] SearchTypeModel reportInput)
        {
            return Ok(_reportsLogic.GetMostUsedTypes(reportInput.StartDate, reportInput.EndDate));
        }
    }
}
