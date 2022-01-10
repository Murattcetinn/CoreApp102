using CoreApp102.Mvc.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp102.Mvc.Filters
{
    public class ValidationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 400;

                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(s => s.Errors);
                modelErrors.ToList().ForEach(s =>
                {
                    errorDto.Errors.Add(s.ErrorMessage);
                });
                //context.Result = new BadRequestObjectResult(errorDto);  
                context.Result = new BadRequestResult();
            }
        }

    }
}
