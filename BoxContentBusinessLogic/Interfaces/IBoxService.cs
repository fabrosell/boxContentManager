using BoxContentCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentBusinessLogic.Interfaces
{
    public interface IBoxService
    {        
        public List<Box> GetUserBoxes(User user);
        public List<Box> SearchBoxes(BoxSearchSettings settings, User user);
        public Box CreateBox(Box box, User user);
        public Box UpdateBox(Box box, User user);
        public Box DeleteBox(Box box, User user);
    }
}
