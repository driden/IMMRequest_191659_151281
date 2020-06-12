namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Logic.Interfaces;
    using Logic.Models.Error;
    using Logic.Models.Topic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsLogic _topicsLogic;

        public TopicsController(ITopicsLogic topicsLogic)
        {
            _topicsLogic = topicsLogic;
        }

        /// <summary>
        ///     Lists all the topics for a given Area
        /// </summary>
        /// <param name="areaId">the area Id for which their topic should be listed</param>
        /// <returns>Returns the list of topics in an area</returns>
        [HttpGet]
        [Route("{areaId}")]
        public ActionResult<IEnumerable<TopicModel>> GetAll(int areaId)
        {
            try
            {
                return Ok(_topicsLogic.GetAll(areaId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }
    }
}
