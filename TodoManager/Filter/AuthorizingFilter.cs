using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using TodoManager.Dal.Cache;
using TodoManager.Domain;
using TodoManager.Models.Dto;
using TodoManager.Models.Enum;

namespace TodoManager.Filter
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public const string AuthorizationHeaderName = "Authorization";
        private IDisctributedCacheProvider cacheProvider;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = GetAccessToken(context);
            if (!string.IsNullOrWhiteSpace(accessToken) && ValidAccessToken(context, accessToken))
            {
                return;
            }
            SimpleResponseDto<bool> simpleResponseDto = SimpleResponseDto<bool>.Failed(ResponseCodeEnum.ResponseCode_102);
            context.Result = new BadRequestObjectResult(simpleResponseDto);
        }

        private bool ValidAccessToken(ActionExecutingContext context, string accessToken)
        {
            context.HttpContext.Items[AuthorizationHeaderName] = accessToken;
            if (cacheProvider == null)
            {
                cacheProvider = (IDisctributedCacheProvider)context.HttpContext.RequestServices.GetService(typeof(IDisctributedCacheProvider));
            }
            //get user from distributed cache
            User user = cacheProvider.Get<User>(accessToken);
            if (user != null)
            {
                //there, we can cache user object to http context.
                return true;
            }
            return false;
        }

        private string GetAccessToken(ActionExecutingContext context)
        {
            StringValues vs;
            if (context.HttpContext.Request.Headers.TryGetValue(AuthorizationHeaderName, out vs))
            {
                return vs.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
