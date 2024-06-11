using BoxContentCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentBusinessLogic.Interfaces
{
    public interface IUserService
    {        
        public User GetUserByEmail(string email);        
        public User CreateUser(User user);
        public User UpdateUser(User user);
        public User DeleteUser(User user);
    }
}
