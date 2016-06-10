using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using lunch.Repositories;

namespace lunch.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AutoSaveChangesAttribute : ActionFilterAttribute
    {
        public bool? AutoSave { get; set; }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (NeedsSave(actionExecutedContext.Request.Method))
            {
                var dbContextOperations = (IDbContextOperations) actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IDbContextOperations));
                
                if (actionExecutedContext.Exception == null)
                    return dbContextOperations.SaveChangesAsync(cancellationToken);
            }

            return Task.CompletedTask;
        }

        private bool NeedsSave(HttpMethod httpMethod)
        {
            if (AutoSave.HasValue)
                return AutoSave.Value;

            return httpMethod == HttpMethod.Post ||
                   httpMethod == HttpMethod.Put ||
                   httpMethod == HttpMethod.Delete;
        }
    }
}