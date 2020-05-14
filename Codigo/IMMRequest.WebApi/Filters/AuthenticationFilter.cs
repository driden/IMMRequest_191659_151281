using System;
using IMMRequest.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IMMRequest.WebApi.Filters
{
    using Logic.Models;

    public class AuthenticationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string headerToken = context.HttpContext.Request.Headers["Authorization"];

            if (headerToken is null)
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse("Token is required"));
            }
            else
            {
                try
                {
                    Guid token = Guid.Parse(headerToken);
                    this.VerifyToken(context, token);
                }
                catch (FormatException)
                {
                    context.Result = new BadRequestObjectResult(new ErrorResponse("Token is required"));
                }
            }
        }

        private void VerifyToken(ActionExecutingContext context, Guid token)
        {
            var session = this.GetSessionLogic(context);
            if (!session.IsValidToken(token, context.HttpContext.Request.Headers["username"]))
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse("Invalid Token"));
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
