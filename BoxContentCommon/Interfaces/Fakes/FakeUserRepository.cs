using BoxContentCommon.Entities;
using BoxContentCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentCommon.Interfaces.Fakes
{
    public class FakeUserRepository : IUserStorageService
    {
        private List<User> users = new List<User>();

        public User Create(User user)
        {
            if (string.IsNullOrWhiteSpace(user.eMail))
                throw new Exception("Please provide a user email");

            if (string.IsNullOrWhiteSpace(user.name))
                throw new Exception("Please provide a user name");

            user.userID = Guid.NewGuid().ToString();
            user.eMail = user.eMail.ToLower();
            user.createdAt = DateTime.UtcNow;
            users.Add(user);
            return user;
        }

        public User Delete(User user)
        {
            if (user.userID == null)
                return null;

            var user_to_delete = users.Where(x => x.userID == user.userID)?.FirstOrDefault();

            if (user_to_delete != null)
                users.Remove(user_to_delete);

            return user_to_delete;
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return users.Where(x => string.Equals(email, x.eMail, StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();
        }

        public User GetByID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return users.Where(x => string.Equals(id, x.userID, StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();
        }

        public User Update(User user)
        {
            if (user.userID == null)
                return null;

            var user_to_update = users.Where(x => x.userID == user.userID)?.FirstOrDefault();

            if (user_to_update != null)
            {
                user_to_update.name = user.name;
                user_to_update.eMail = user.eMail;
                user_to_update.lastUpdatedAt = DateTime.UtcNow;
            }

            return user_to_update;
        }
    }
}
