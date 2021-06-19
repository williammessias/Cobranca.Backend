using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;

namespace Application.Filters
{
    public class ModelStateValidatorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                var errors = new ListaErroResponseDto(context.ModelState.GetErrorsMessages());

                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }

    public static class ModelStateExtensions
    {
        public static List<string> GetErrorsMessages(this ModelStateDictionary modelState)
        {
            return modelState
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
        }
    }
}
