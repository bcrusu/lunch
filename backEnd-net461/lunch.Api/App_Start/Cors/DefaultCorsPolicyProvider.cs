using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;
using lunch.Configuration;

namespace lunch.Api.Cors
{
    public class DefaultCorsPolicyProvider : ICorsPolicyProvider
    {
        private EnableCorsAttribute _enableCorsAttribute;

        Task<CorsPolicy> ICorsPolicyProvider.GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_enableCorsAttribute == null)
                _enableCorsAttribute = GetEnableCorsAttribute();

            return _enableCorsAttribute.GetCorsPolicyAsync(request, cancellationToken);
        }

        private EnableCorsAttribute GetEnableCorsAttribute()
        {
            var origins = ApplicationSettings.DefaultAccessControlAllowOrigin;
            var result = new EnableCorsAttribute(origins, "*", "*")
            {
                SupportsCredentials = true,
                PreflightMaxAge = 5 * 60  // 5 minutes
            };

            return result;
        }
    }
}