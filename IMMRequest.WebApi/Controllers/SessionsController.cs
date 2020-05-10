using System;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    using Logic.Exceptions;

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
        public IActionResult Login([FromBody]ModelAdminLogin adminLogin)
        {
            try
            {
                return Ok(new { Token = this._sessionLogic.Login(adminLogin) });
            }
            catch (NoSuchAdministrator noSuchAdministrator)
            {
                return BadRequest(new ErrorResponse(noSuchAdministrator.Message));
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse("An error occurred while logging you in, please try again"));
            }
        }
    }
}
