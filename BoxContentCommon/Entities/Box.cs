using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentCommon.Entities
{
    public class Box
    {
        public string boxID { get; set; }
        public string name { get; set; }
        public List<string> items { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastUpdatedAt { get; set; }
        public string ownerID { get; set; }
    }

    public enum BoxSearchType
    {
        SearchByName,
        SearchByItems,
        SearchByAll
    }

    public class BoxSearchSettings
    {
        public string searchTerm { get; set; }
        public bool exactMatch { get; set; }
        public BoxSearchType searchType { get; set; }
    }
}
