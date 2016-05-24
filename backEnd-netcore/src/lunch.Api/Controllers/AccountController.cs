using System.Threading.Tasks;
using lunch.Api.Models.Account;
using lunch.BusinessLogic.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using lunch.Api.Internal.Auth;

namespace lunch.Api.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger<AccountController>();

        private readonly IUserSessionBusinessLogic _userSessionBusinessLogic;
        private readonly IJwtSecurityTokenFactory _jwtSecurityTokenFactory;

        public AccountController(IUserSessionBusinessLogic userSessionBusinessLogic,
            IJwtSecurityTokenFactory jwtSecurityTokenFactory)
        {
            _userSessionBusinessLogic = userSessionBusinessLogic;
            _jwtSecurityTokenFactory = jwtSecurityTokenFactory;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignInLinkedin(SignInLinkedinModel model)
        {
            this.CheckModelStateIsValid();

            string token;
            var userSession = await this.GetCurrentUserSession();
            if (userSession == null)
            {
                var externalUserDetails = await LinkedinUserDetailsProvider.GetUserDetails(HttpContext.RequestServices, model, Request.HttpContext.RequestAborted);
                userSession = await _userSessionBusinessLogic.CreateSessionForExternalUser(externalUserDetails);

                token = _jwtSecurityTokenFactory.Create(userSession);
            }
            else
            {
                Log.InfoFormat("User '{0}' is already signed-in. Reusing existing session.", userSession.UserId);

                // return the bearer token received
                //TODO: token = Request.Headers. Authorization.Parameter;
                token = "TODO";
            }

            var result = new SignInResultModel
            {
                Token = token
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SignOutCurrentSession()
        {
            var userSession = await this.GetCurrentUserSession();

            if (userSession != null)
                _userSessionBusinessLogic.CloseSession(userSession);

            return Ok();
        }
    }
}
