namespace IMMRequest.WebApi.Controllers
{
    using System.Collections.Generic;
    using Domain;
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Admin;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/admins")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    [AuthorizationFilter]
    [DomainExceptionFilter]
    [LogicExceptionFilter]
    [SystemExceptionFilter]
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
            return Ok(_adminsLogic.GetAll());
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, [FromBody] AdminModel modifyRequest)
        {
            _adminsLogic.Update(id, modifyRequest);
            return NoContent();
        }

        [HttpPost]
        public ActionResult Add([FromBody] AdminModel addRequest)
        {
            return Ok(new { Id = _adminsLogic.Add(addRequest) });
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            _adminsLogic.Remove(id);
            return Ok();
        }
    }
}
