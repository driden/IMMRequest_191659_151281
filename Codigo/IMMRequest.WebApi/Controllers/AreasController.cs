namespace IMMRequest.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Logic.Interfaces;
    using Logic.Models.Area;
    using Logic.Models.Error;
    using Microsoft.AspNetCore.Http;

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
        public ActionResult<IEnumerable<AreaModel>> GetAll()
        {
            try
            {
                return Ok(_areasLogic.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(ex.Message));
            }
        }
    }
}
