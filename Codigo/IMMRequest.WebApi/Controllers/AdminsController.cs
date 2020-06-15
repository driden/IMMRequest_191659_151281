namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Filters;
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
        [DomainExceptionFilter]
        [LogicExceptionFilter]
        public ActionResult Update(int id, [FromBody] AdminModel modifyRequest)
        {
            try
            {
                _adminsLogic.Update(id, modifyRequest);
                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ErrorModel(exception.Message));
            }
        }

        [HttpPost]
        [DomainExceptionFilter]
        [LogicExceptionFilter]
        public ActionResult Add([FromBody] AdminModel addRequest)
        {
            try
            {
                return Ok(new { Id = _adminsLogic.Add(addRequest) });
            }
            catch (Exception exception)
            {
                return StatusCode(500, new ErrorModel(exception.Message));
            }
        }

        [HttpDelete]
        [LogicExceptionFilter]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            try
            {
                _adminsLogic.Remove(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorModel(exception.Message));
            }
        }
    }
}
