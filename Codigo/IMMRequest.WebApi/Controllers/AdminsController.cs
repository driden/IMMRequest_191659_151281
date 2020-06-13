namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Domain.Exceptions;
    using Filters;
    using Logic.Exceptions.RemoveType;
    using Logic.Interfaces;
    using Logic.Models.Admin;
    using Logic.Models.Error;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/admins")]
    [ApiController]
    [AuthorizationFilter]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminsLogic _adminsLogic;

        public AdminsController(IAdminsLogic adminsLogic)
        {
            _adminsLogic = adminsLogic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Admin>> GetAll()
        {
            try
            {
                return Ok(_adminsLogic.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel(ex.Message));
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, [FromBody] AdminModel modifyRequest)
        {
            try
            {
                _adminsLogic.Update(id, modifyRequest);
                return NoContent();
            }
            catch (InvalidIdException invalidIdException)
            {
                return BadRequest(new ErrorModel(invalidIdException.Message));
            }
            catch (InvalidEmailException invalidEmailException)
            {
                return BadRequest(new ErrorModel(invalidEmailException.Message));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ErrorModel(exception.Message));
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] AdminModel addRequest)
        {
            try
            {
                return Ok(new {Id = _adminsLogic.Add(addRequest)});
            }
            catch (InvalidNameFormatException invalidNameFormatException)
            {
                return BadRequest(new ErrorModel(invalidNameFormatException.Message));
            }
            catch (InvalidEmailException emailException)
            {
                return BadRequest(new ErrorModel(emailException.Message));
            }
            catch (InvalidPhoneNumberException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
            catch (InvalidPasswordException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
            catch (InvalidIdException exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ErrorModel(exception.Message));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            try
            {
                _adminsLogic.Remove(id);
                return Ok();
            }
            catch (InvalidIdException invalidIdException)
            {
                return BadRequest(new ErrorModel(invalidIdException.Message));
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
        }
    }
}
