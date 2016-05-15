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
            this.CheckModelStateIsValid();

            //TODO: check if user already logged-in

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
    }
}
