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
        private readonly ISessionLogic sessionLogic;

        public SessionController(ISessionLogic _sessionLogic)
        {
            this.sessionLogic = _sessionLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody]ModelAdminLogin _adminLogin)
        {
            try
            {
                return Ok(this.sessionLogic.Login(_adminLogin.Email, _adminLogin.Password));
            }
            catch(Exception)
            {
                return BadRequest("Error credentials");
            }
            
        }
    }
}
