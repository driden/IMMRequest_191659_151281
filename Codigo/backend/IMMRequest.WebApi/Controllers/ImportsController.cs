namespace IMMRequest.WebApi.Controllers
{
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Files;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/imports")]
    [ApiController]
    [SystemExceptionFilter]
    [DomainExceptionFilter]
    [LogicExceptionFilter]
    public class ImportsController : ControllerBase
    {
        private readonly IImportsLogic _importsLogic;
        public ImportsController(IImportsLogic importsLogic)
        {
            _importsLogic = importsLogic;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult<int[]> Import([FromBody] FileRequestModel fileRequest)
        {
            return Ok(_importsLogic.Import(fileRequest));
        }
    }
}
