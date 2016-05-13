using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using lunch.Repositories;

namespace lunch.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AutoSaveChangesAttribute : ActionFilterAttribute, IActionFilter
    {
        public bool Enabled { get; set; } = true;

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var dbContextOperations = (IDbContextOperations)actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IDbContextOperations));

            if (Enabled)
                return dbContextOperations.SaveChangesAsync(cancellationToken);

            return Task.FromResult(0);
        }
    }
}