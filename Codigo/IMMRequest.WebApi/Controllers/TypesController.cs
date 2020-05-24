using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    using Domain.Exceptions;
    using Logic.Exceptions;
    using Logic.Exceptions.CreateTopic;
    using Logic.Exceptions.RemoveType;
    using Logic.Interfaces;
    using Logic.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Filters.AuthenticationFilter]
    public class TypesController : ControllerBase
    {
        private readonly ITypesLogic _typesLogic;

        public TypesController(ITypesLogic typesLogic)
        {
            _typesLogic = typesLogic;
        }

        /// <summary>
        /// Creates a new type in the system
        /// </summary>
        /// <param name="request">request body</param>
        /// <response code="200">Type created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpPost]
        public ActionResult AddType([FromBody] CreateTypeRequest request)
        {
            try
            {
                var typeId = _typesLogic.Add(request);
                return new OkObjectResult(new {Id = typeId, Text = $"Type created with id {typeId}"});
            }
            catch (InvalidTopicIdException invalidTopicIdException)
            {
                return BadRequest(new ErrorResponse(invalidTopicIdException.Message));
            }
            catch (InvalidFieldTypeException invalidFieldTypeException)
            {
                return BadRequest(new ErrorResponse(invalidFieldTypeException.Message));
            }
            catch (NoSuchTopicException suchTopicException)
            {
                return BadRequest(new ErrorResponse(suchTopicException.Message));
            }
            catch (EmptyTypeNameException emptyTypeNameException)
            {
                return BadRequest(new ErrorResponse(emptyTypeNameException.Message));
            }
            catch (ExistingTypeNameException existingTypeNameException)
            {
                return BadRequest(new ErrorResponse(existingTypeNameException.Message));
            }
            catch (InvalidFieldValueCastForFieldTypeException invalidFieldValueCastForFieldTypeException)
            {
                return BadRequest(new ErrorResponse(invalidFieldValueCastForFieldTypeException.Message));
            }
            catch (InvalidFieldRangeException invalidFieldRangeException)
            {
                return BadRequest(new ErrorResponse(invalidFieldRangeException.Message));
            }
            catch (InvalidNameForAdditionalFieldException invalidNameForAdditionalField)
            {
                return BadRequest(new ErrorResponse(invalidNameForAdditionalField.Message));
            }
            catch (InvalidAdditionalFieldForTypeException exception)
            {
                return BadRequest(new ErrorResponse(exception.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(ex.Message));
            }
        }

        /// <summary>
        /// Creates a new type in the system
        /// </summary>
        /// <param name="id">Type id</param>
        /// <response code="200">Type created</response>
        /// <response code="400">There's something wrong with the request body</response>
        /// <response code="500">Something is wrong with the server</response>
        [HttpDelete]
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
                return BadRequest(new ErrorResponse(invalidTypeIdException.Message));
            }
            catch (NoSuchTypeException noSuchTypeException)
            {
                return BadRequest(new ErrorResponse(noSuchTypeException.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(ex.Message));
            }
        }
    }
}
