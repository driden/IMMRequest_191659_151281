namespace IMMRequest.WebApi.Controllers
{
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Admin;
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
        [LogicExceptionFilter]
        public IActionResult Login([FromBody] AdminLoginModel adminLogin)
        {
            return Ok(new { Token = _sessionLogic.Login(adminLogin) });
        }
    }
}
