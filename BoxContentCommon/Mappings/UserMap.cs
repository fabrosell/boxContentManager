using BoxContentCommon.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace BoxContentCommon.Mappings
{
    public class UserMap
    {
        public static void Map()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(User)))
                return;

            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.userID).SetIdGenerator(StringObjectIdGenerator.Instance);
                map.MapMember(x => x.eMail).SetIsRequired(true);
                map.MapMember(x => x.name).SetIsRequired(true);
                map.MapMember(x => x.createdAt).SetIsRequired(true);
                map.MapMember(x => x.lastUpdatedAt).SetIsRequired(true);
            });
        }
    }
}
