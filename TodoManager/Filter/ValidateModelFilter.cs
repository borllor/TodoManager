using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoManager.Models;
using TodoManager.Models.Dto;
using TodoManager.Models.Enum;

namespace TodoManager.Filter
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                SimpleResponseDto<IList<ValidationError>> simpleResponseDto = SimpleResponseDto<IList<ValidationError>>.Failed(ResponseCodeEnum.ResponseCode_101);

                simpleResponseDto.Result = context.ModelState.Keys
                .SelectMany(key => context.ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();

                context.Result = new BadRequestObjectResult(simpleResponseDto);
            }
        }
    }
}
