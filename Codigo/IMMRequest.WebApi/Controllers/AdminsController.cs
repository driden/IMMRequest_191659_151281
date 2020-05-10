using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain;
    using Domain.Exceptions;
    using Logic.Exceptions.RemoveType;
    using Logic.Interfaces;
    using Logic.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Filters.AuthenticationFilter]
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
                return StatusCode(500, new ErrorResponse(ex.Message));
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, [FromBody] CreateAdminRequest modifyRequest)
        {
            try
            {
                _adminsLogic.Update(id, modifyRequest);
                return Ok();
            }
            catch (InvalidIdException invalidIdException)
            {
                return BadRequest(new ErrorResponse(invalidIdException.Message));
            }
            catch (InvalidEmailException invalidEmailException)
            {
                return BadRequest(new ErrorResponse(invalidEmailException.Message));
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorResponse(exception.Message));
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] CreateAdminRequest addRequest)
        {
            try
            {
                return Ok(new { Id = _adminsLogic.Add(addRequest) });
            }
            catch (InvalidNameFormatException invalidNameFormatException)
            {
                return BadRequest(new ErrorResponse(invalidNameFormatException.Message));
            }
            catch (InvalidEmailException emailException)
            {
                return BadRequest(new ErrorResponse(emailException.Message));
            }
            catch (InvalidPhoneNumberException exception)
            {
                return BadRequest(new ErrorResponse(exception.Message));
            }
            catch (InvalidPasswordException exception)
            {
                return BadRequest(new ErrorResponse(exception.Message));
            }
            catch (InvalidIdException exception)
            {
                return BadRequest(new ErrorResponse(exception.Message));
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorResponse(exception.Message));
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
                return BadRequest(new ErrorResponse(invalidIdException.Message));
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorResponse(exception.Message));
            }
        }

    }
}
