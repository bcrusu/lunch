using System.Net;
using System.Web.Http;

namespace lunch.Api
{
    internal static class WebApiExtensions
    {
        public static void CheckModelStateIsValid(this ApiController target)
        {
            if (!target.ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}