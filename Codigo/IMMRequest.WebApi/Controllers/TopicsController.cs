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

        /// <summary>
        ///     Creates a new topic in the system
        /// </summary>
        /// <param name="topicModel">topic body</param>
        /// <response code="201">Topic created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="404">A field name could not be found</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPost]
        public ActionResult CreateTopic([FromBody] TopicModel topicModel)
        {
            var topicId = _topicsLogic.Add(topicModel); 
            return CreatedAtRoute(
                new { Id = topicId },
                new { Id = topicId, Text = $"topic created with id {topicId}" });
        }
    }
}
