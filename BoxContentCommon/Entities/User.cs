using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentCommon.Entities
{
    public class User
    {
        public string userID { get; set; }
        public string name { get; set; }
        public string eMail { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? lastUpdatedAt { get; set; }
    }
}
