using BoxContentBusinessLogic.Interfaces;
using BoxContentCommon.Entities;
using BoxContentCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentBusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserStorageService _userStorageService;

        public UserService(IUserStorageService userStorageService) { 
            this._userStorageService = userStorageService; 
        }

        public User CreateUser(User user)
        {
            return _userStorageService.Create(user);            
        }

        public User DeleteUser(User user)
        {
            // TODO: Cannot delete user with boxes
            return _userStorageService.Delete(user);
        }

        public User GetUserByEmail(string email)
        {
            return _userStorageService.GetByEmail(email);
        }

        public User UpdateUser(User user)
        {
            return _userStorageService.Update(user);
        }
    }
}
