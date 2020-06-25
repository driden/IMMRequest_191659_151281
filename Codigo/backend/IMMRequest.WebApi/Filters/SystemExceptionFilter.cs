namespace IMMRequest.WebApi.Filters
{
    using System.Net;
    using Logic.Models.Error;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class SystemExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new BadRequestObjectResult(new ErrorModel(context.Exception.Message));
        }
    }
}
