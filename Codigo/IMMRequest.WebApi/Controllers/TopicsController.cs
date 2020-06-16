namespace IMMRequest.WebApi.Controllers
{
    using System.Collections.Generic;
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Topic;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/topics")]
    [ApiController]
    [SystemExceptionFilter]
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
            return Ok(_topicsLogic.GetAll(areaId));
        }
    }
}
