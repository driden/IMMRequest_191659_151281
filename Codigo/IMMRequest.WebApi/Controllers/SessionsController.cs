namespace IMMRequest.WebApi.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Logic.Interfaces;
    using Logic.Exceptions;
    using Logic.Models.Admin;
    using Logic.Models.Error;

    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionLogic _sessionLogic;

        public SessionsController(ISessionLogic sessionLogic)
        {
            this._sessionLogic = sessionLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody]AdminLoginModel adminLogin)
        {
            try
            {
                return Ok(new { Token = this._sessionLogic.Login(adminLogin) });
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
