using BoxContentCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentTests
{
    public class BoxContentTestUtilities
    {
        public static User GetDummyUser()
        {            
            var email = $"john@test.{DateTime.UtcNow.Ticks.ToString()}.cl";

            return new User()
            {
                name = "John Test",
                eMail = email,
                createdAt = DateTime.UtcNow,
                userID = null
            };
        }

        public static Box GetDummyBox(User owner)
        {
            var randomizer = new Random();

            var items = new List<string>();

            // Add a random amount of items up to 20
            for (int i = 1; i <= randomizer.Next(1, 20); i++)
                items.Add($"Item #{i}");
            
            return new Box()
            {
                name = $"Big box #{randomizer.Next()} GUID {Guid.NewGuid().ToString()}",
                createdAt = DateTime.UtcNow,
                items = items,
                ownerID = owner.userID
            };
        }
    }
}
