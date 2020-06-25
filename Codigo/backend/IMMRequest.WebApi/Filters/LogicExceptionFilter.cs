namespace IMMRequest.WebApi.Filters
{
    using System.Linq;
    using System.Net;
    using Logic.Exceptions;
    using Logic.Models.Error;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class LogicExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logicExceptions = new[]
           {
                typeof(AccountException),
                typeof(AdditionalFieldException),
                typeof(RequestException),
                typeof(TopicException),
                typeof(TypeException),
            };

            var exception = context.Exception.GetType();

            if (logicExceptions.Contains(exception))
            {
                context.ExceptionHandled = true;
                HttpResponse response = context.HttpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ErrorModel(context.Exception.Message));
            }
        }
    }
}
