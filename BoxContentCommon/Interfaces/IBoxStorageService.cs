using BoxContentCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentCommon.Interfaces
{
    public interface IBoxStorageService
    {
        public Box GetByID(string id);
        public List<Box> GetByUser(string userId);
        public List<Box> Search(BoxSearchSettings settings, User user);
        public Box Create(Box box);
        public Box Update(Box box);
        public Box Delete(Box box);
    }
}
