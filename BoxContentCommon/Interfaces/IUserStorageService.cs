using BoxContentCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentCommon.Interfaces
{
    public interface IUserStorageService
    {
        public User GetByEmail(string email);
        public User GetByID(string id);
        public User Create(User user);
        public User Update(User user);
        public User Delete(User user);
    }
}
