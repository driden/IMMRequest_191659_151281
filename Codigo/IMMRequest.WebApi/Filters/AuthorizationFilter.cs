namespace IMMRequest.WebApi.Filters
{
    using System;
    using Logic.Interfaces;
    using Logic.Models.Error;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class AuthorizationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string headerToken = context.HttpContext.Request.Headers["Authorization"];

            if (headerToken is null)
            {
                context.Result = new BadRequestObjectResult(new ErrorModel("Token is required"));
            }
            else
            {
                try
                {
                    Guid token = Guid.Parse(headerToken);
                    VerifyToken(context, token);
                }
                catch (FormatException)
                {
                    context.Result = new BadRequestObjectResult(new ErrorModel("Token is required"));
                }
            }
        }

        private void VerifyToken(ActionExecutingContext context, Guid token)
        {
            var session = GetSessionLogic(context);
            if (!session.IsValidToken(token, context.HttpContext.Request.Headers["username"]))
            {
                context.Result = new BadRequestObjectResult(new ErrorModel("Invalid Token"));
            }
            else
            {
                int userLoginId = 0;
                context.ActionArguments.Add("userLoggedId", userLoginId);
            }
        }

        private ISessionLogic GetSessionLogic(ActionExecutingContext context)
        {
            return context.HttpContext.RequestServices.GetService(typeof(ISessionLogic)) as ISessionLogic;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Nothing for now
        }
    }
}
