using BoxContentBusinessLogic.Interfaces;
using BoxContentCommon.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BoxContentAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet(Name = "GetByEmail")]
        public User GetByEmail(string email)
        {
            return this._userService.GetUserByEmail(email);
        }

        [HttpPost]
        public User CreateUser(User user)
        {
            // TODO: validate user before calling the service

            return this._userService.CreateUser(user);
        }

        [HttpDelete]
        public User DeleteUser(User user)
        {
            // Cannot delete user with boxes

            return this._userService.DeleteUser(user);
        }

        [HttpPut]
        public User UpdateUser(User user)
        {
            // TODO: validate user before calling the service

            return this._userService.UpdateUser(user);
        }
    }
}
