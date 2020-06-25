namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Filters;
    using IMMRequest.Logic.Models.Type;
    using Logic.Interfaces;
    using Logic.Models.State;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/reports")]
    [EnableCors("CorsPolicy")]
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
        /// <param name="mail">Mail from Citizen</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>A json object with the details of the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StateReportModel>), 200)]
        [Route("a")]
        [AuthorizationFilter]
        public ActionResult<IEnumerable<StateReportModel>> GetAllRequestByMail(string mail, DateTime startDate, DateTime endDate)
        {
            return Ok(_reportsLogic.GetRequestByMail(mail, startDate, endDate));
        }

        /// <summary>
        ///     Lists all the most used type for requests
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Returns the list of types in a topic</returns>
        [HttpGet]
        [Route("b")]
        [AuthorizationFilter]
        public ActionResult<IEnumerable<TypeReportModel>> GetMostUsedTypes(DateTime startDate, DateTime endDate)
        {
            return Ok(_reportsLogic.GetMostUsedTypes(startDate, endDate));
        }
    }
}
