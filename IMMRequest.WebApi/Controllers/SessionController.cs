using System;
using IMMRequest.Logic.Interfaces;
using IMMRequest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[sessions]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic _sessionLogic;

        public SessionController(ISessionLogic sessionLogic)
        {
            this._sessionLogic = sessionLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody]ModelAdminLogin adminLogin)
        {
            try
            {
                return Ok(this._sessionLogic.Login(adminLogin.Email, adminLogin.Password));
            }
            catch(Exception)
            {
                return BadRequest("Error credentials");
            }

        }
    }
}
