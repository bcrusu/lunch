using lunch.Api.Models.UserProfile;
using lunch.BusinessLogic.Security;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lunch.Api.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserBusinessLogic _userBusinessLogic;

        public UserProfileController(IUserBusinessLogic userBusinessLogic)
        {
            _userBusinessLogic = userBusinessLogic;
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            var userSession = await this.GetCurrentUserSession();

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
