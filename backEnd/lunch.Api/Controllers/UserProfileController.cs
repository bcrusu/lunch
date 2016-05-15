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
            this.CheckModelStateIsValid();

            var result = new UserInfoModel
            {
                DisplayName = "test",
                SmallPictureUrl = "xx"
            };

            return Ok(result);
        }
    }
}
