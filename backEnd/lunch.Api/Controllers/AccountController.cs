using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using lunch.Api.Auth;
using lunch.Api.Models.Account;
using lunch.BusinessLogic.Security;

namespace lunch.Api.Controllers
{
    public class AccountController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger<AccountController>();

        private readonly IUserSessionBusinessLogic _userSessionBusinessLogic;

        public AccountController(IUserSessionBusinessLogic userSessionBusinessLogic)
        {
            _userSessionBusinessLogic = userSessionBusinessLogic;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SignInLinkedin(SignInLinkedinModel model)
        {
            this.CheckModelStateIsValid();

            string token;
            var userSession = this.GetCurrentUserSession();
            if (userSession == null)
            {
                var externalUserDetails = await LinkedinUserDetailsProvider.GetUserDetails(model, Request.GetOwinContext().Request.CallCancelled);
                userSession = _userSessionBusinessLogic.CreateSessionForExternalUser(externalUserDetails);

                token = JwtHelper.Create(userSession);
            }
            else
            {
                Log.InfoFormat("User '{0}' is already signed-in. Reusing existing session.", userSession.UserId);

                // return the bearer token received
                token = Request.Headers.Authorization.Parameter;
            }

            var result = new SignInResultModel
            {
                Token = token
            };

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult SignOutCurrentSession()
        {
            var userSession = this.GetCurrentUserSession();

            if (userSession != null)
                _userSessionBusinessLogic.CloseSession(userSession);

            return Ok();
        }
    }
}
