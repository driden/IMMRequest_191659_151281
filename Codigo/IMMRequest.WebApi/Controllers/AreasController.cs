namespace IMMRequest.WebApi.Controllers
{
    using System.Collections.Generic;
    using Filters;
    using Logic.Interfaces;
    using Logic.Models.Area;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/areas")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreasLogic _areasLogic;

        public AreasController(IAreasLogic areasLogic)
        {
            _areasLogic = areasLogic;
        }

        [HttpGet]
        [SystemExceptionFilter]
        public ActionResult<IEnumerable<AreaModel>> GetAll()
        {
            return Ok(_areasLogic.GetAll());
        }
    }
}
