namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Error;
    using Logic.Models.Type;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/types")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ITypesLogic _typesLogic;

        public TypesController(ITypesLogic typesLogic)
        {
            _typesLogic = typesLogic;
        }

        /// <summary>
        ///     Creates a new type in the system
        /// </summary>
        /// <param name="request">request body</param>
        /// <response code="200">Type created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPost]
        [AuthorizationFilter]
        [DomainExceptionFilter]
        [LogicExceptionFilter]
        public ActionResult AddType([FromBody] CreateTypeRequest request)
        {
            try
            {
                var typeId = _typesLogic.Add(request);
                return new OkObjectResult(new { Id = typeId, Text = $"Type created with id {typeId}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }

        /// <summary>
        ///     Creates a new type in the system
        /// </summary>
        /// <param name="id">Type id</param>
        /// <response code="200">Type created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpDelete]
        [AuthorizationFilter]
        [Route("{id}")]
        [LogicExceptionFilter]
        public ActionResult DeleteType(int id)
        {
            try
            {
                _typesLogic.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }

        /// <summary>
        ///     Lists all the types for a given topic.
        /// </summary>
        /// <param name="topicId">the topic Id for which their type should be listed</param>
        /// <returns>Returns the list of types in a topic</returns>
        [HttpGet]
        [Route("{topicId}")]
        public IEnumerable<TypeModel> GetAll(int topicId)
        {
            return _typesLogic.GetAll(topicId);
        }
    }
}
