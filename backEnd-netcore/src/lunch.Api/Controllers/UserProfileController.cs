using System;
using lunch.Api.Models.UserProfile;
using lunch.BusinessLogic.Security;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult UserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
