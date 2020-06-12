namespace IMMRequest.WebApi.Controllers
{
    using System;
    using Logic.Exceptions;
    using Logic.Interfaces;
    using Logic.Models.Admin;
    using Logic.Models.Error;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/sessions")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionLogic _sessionLogic;

        public SessionsController(ISessionLogic sessionLogic)
        {
            _sessionLogic = sessionLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AdminLoginModel adminLogin)
        {
            try
            {
                return Ok(new {Token = _sessionLogic.Login(adminLogin)});
            }
            catch (NoSuchAdministrator noSuchAdministrator)
            {
                return BadRequest(new ErrorModel(noSuchAdministrator.Message));
            }
            catch (Exception)
            {
                return BadRequest(new ErrorModel("An error occurred while logging you in, please try again"));
            }
        }
    }
}
