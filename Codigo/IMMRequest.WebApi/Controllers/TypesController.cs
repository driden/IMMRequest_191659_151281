namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Domain.Exceptions;
    using Filters;
    using Logic.Exceptions;
    using Logic.Exceptions.CreateTopic;
    using Logic.Exceptions.RemoveType;
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
        public ActionResult AddType([FromBody] CreateTypeRequest request)
        {
            try
            {
                var typeId = _typesLogic.Add(request);
                return new OkObjectResult(new {Id = typeId, Text = $"Type created with id {typeId}"});
            }
            catch (InvalidTopicIdException invalidTopicIdException)
            {
                return BadRequest(new ErrorModel(invalidTopicIdException.Message));
            }
            catch (InvalidFieldTypeException invalidFieldTypeException)
            {
                return BadRequest(new ErrorModel(invalidFieldTypeException.Message));
            }
            catch (NoSuchTopicException suchTopicException)
            {
                return BadRequest(new ErrorModel(suchTopicException.Message));
            }
            catch (EmptyTypeNameException emptyTypeNameException)
            {
                return BadRequest(new ErrorModel(emptyTypeNameException.Message));
            }
            catch (ExistingTypeNameException existingTypeNameException)
            {
                return BadRequest(new ErrorModel(existingTypeNameException.Message));
            }
            catch (InvalidFieldValueCastForFieldTypeException invalidFieldValueCastForFieldTypeException)
            {
                return BadRequest(new ErrorModel(invalidFieldValueCastForFieldTypeException.Message));
            }
            catch (InvalidFieldRangeException invalidFieldRangeException)
            {
                return BadRequest(new ErrorModel(invalidFieldRangeException.Message));
            }
            catch (InvalidNameForAdditionalFieldException invalidNameForAdditionalField)
            {
                return BadRequest(new ErrorModel(invalidNameForAdditionalField.Message));
            }
            catch (InvalidAdditionalFieldForTypeException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
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
        public ActionResult DeleteType(int id)
        {
            try
            {
                _typesLogic.Remove(id);
                return Ok();
            }
            catch (InvalidIdException invalidTypeIdException)
            {
                return BadRequest(new ErrorModel(invalidTypeIdException.Message));
            }
            catch (NoSuchTypeException noSuchTypeException)
            {
                return BadRequest(new ErrorModel(noSuchTypeException.Message));
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
