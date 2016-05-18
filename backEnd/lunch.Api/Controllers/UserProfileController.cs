using System.Web.Http;
using lunch.Api.Models.UserProfile;
using lunch.BusinessLogic.Security;

namespace lunch.Api.Controllers
{
    public class UserProfileController : ApiController
    {
        private readonly IUserBusinessLogic _userBusinessLogic;

        public UserProfileController(IUserBusinessLogic userBusinessLogic)
        {
            _userBusinessLogic = userBusinessLogic;
        }

        [HttpGet]
        public IHttpActionResult UserInfo()
        {
            var userSession = this.GetCurrentUserSession();

            var result = new UserInfoModel
            {
                FirstName = userSession.User.FirstName,
                DisplayName = userSession.User.DisplayName,
                PictureUrl = userSession.User.PictureUrl
            };

            return Ok(result);
        }
    }
}
