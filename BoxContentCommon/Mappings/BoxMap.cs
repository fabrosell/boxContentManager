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
    public class BoxMap
    {
        public static void Map()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(Box)))
                return;

            BsonClassMap.RegisterClassMap<Box>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.boxID).SetIdGenerator(StringObjectIdGenerator.Instance);
                map.MapMember(x => x.items).SetIsRequired(true);
                map.MapMember(x => x.name).SetIsRequired(true);
                map.MapMember(x => x.ownerID).SetIsRequired(true);
                map.MapMember(x => x.createdAt).SetIsRequired(true);
                map.MapMember(x => x.lastUpdatedAt).SetIsRequired(true);
            });
        }
    }
}
