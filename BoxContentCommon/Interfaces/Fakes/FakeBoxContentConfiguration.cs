using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxContentCommon.Interfaces;

namespace BoxContentCommon.Interfaces.Fakes
{
    public class FakeBoxContentConfiguration : IBoxContentConfiguration
    {

        public string GetBoxCollectionName()
        {
            return "boxes";
        }

        public string GetConnectionString()
        {
            return "mongodb://127.0.0.1:27017/?direct_connection=true&serverSelectionTimeoutMS=2000";
        }

        public string GetDatabaseName()
        {
            return "boxcontent";
        }

        public string GetUserCollectionName()
        {
            return "users";
        }
    }
}
