namespace IMMRequest.WebApi.Filters
{
    using System.Linq;
    using Domain.Exceptions;
    using Logic.Models.Error;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class DomainExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var domainExceptions = new[]
            {
                typeof(InvalidDetailsException),
                typeof(InvalidEmailException),
                typeof(InvalidNameFormatException),
                typeof(InvalidPasswordException),
                typeof(InvalidPhoneNumberException),
                typeof(InvalidStateException),
                typeof(InvalidFieldRangeException)
            };

            var exception = context.Exception.GetType();

            if (domainExceptions.Contains(exception))
            {
                context.ExceptionHandled = true;
                HttpResponse response = context.HttpContext.Response;
                response.ContentType = "application/json";
                context.Result = new BadRequestObjectResult(new ErrorModel(context.Exception.Message));
            }

            base.OnException(context);
        }
    }
}
