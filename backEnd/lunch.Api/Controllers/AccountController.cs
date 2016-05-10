using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using lunch.Api.Auth;
using Microsoft.AspNet.Identity;
using lunch.Api.Models.Account;
using lunch.BusinessLogic.Security;

namespace lunch.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserBusinessLogic _userBusinessLogic;
        private readonly IUserSessionBusinessLogic _userSessionBusinessLogic;

        public AccountController(IUserBusinessLogic userBusinessLogic, IUserSessionBusinessLogic userSessionBusinessLogic)
        {
            _userBusinessLogic = userBusinessLogic;
            _userSessionBusinessLogic = userSessionBusinessLogic;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> LoginLinkedin(LoginLinkedinModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var externalUserDetails = await LinkedinUserDetailsProvider.GetUserDetails(model, Request.GetOwinContext().Request.CallCancelled);

            var user = _userBusinessLogic.FindByEmail(externalUserDetails.Email);
            if (user == null)
                user = _userBusinessLogic.CreateUser(externalUserDetails);

            var userSession = _userSessionBusinessLogic.CreateSession(user);

            var result = new LoginResultModel
            {
                Token = JwtHelper.Create(userSession)
            };

            return Ok(result);
        }

        #region Helpers

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion
    }
}
