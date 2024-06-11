using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentCommon.Interfaces
{
    public interface IBoxContentConfiguration
    {
        public string GetConnectionString();
        public string GetDatabaseName();
        public string GetUserCollectionName();
        public string GetBoxCollectionName();
    }
}
