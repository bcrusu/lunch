using lunch.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lunch.Api.Internal.Mvc.Filters
{
    public class AutoSaveChangesAttribute : ActionFilterAttribute
    {
        private static readonly ISet<string> AutoSaveForMethods = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "POST",
            "PUT",
            "DELETE"
        };

        public bool? AutoSave { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var context2 = await next();

            if (NeedsSave(context2))
            {
                var dbContextOperations = context.HttpContext.RequestServices.GetService<IDbContextOperations>();

                await dbContextOperations.SaveChangesAsync(context.HttpContext.RequestAborted);
            }
        }

        private bool NeedsSave(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return false;

            if (AutoSave.HasValue)
                return AutoSave.Value;

            var method = context.HttpContext.Request.Method;
            return AutoSaveForMethods.Contains(method);
        }
    }
}
