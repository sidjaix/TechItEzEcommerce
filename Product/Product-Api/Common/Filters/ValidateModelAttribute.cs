using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductApi.Common.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (!filterContext.ModelState.IsValid)
        {
            filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
        }
    }
}
