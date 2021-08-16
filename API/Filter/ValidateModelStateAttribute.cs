using System.Collections.Generic;
using System.Linq;
using Application.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

            var response = Response<List<string>>.MakeResponse(false, "Validation Error", errors,400);

            context.Result = new JsonResult(response)
            {
                StatusCode = 400
            };
        }
    }
}