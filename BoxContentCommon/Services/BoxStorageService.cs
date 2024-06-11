using BoxContentCommon.Entities;
using BoxContentCommon.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxContentCommon.Services
{
    public class BoxStorageService : IBoxStorageService
    {
        private readonly IBoxContentConfiguration _configuration;
        private readonly MongoClient _client;

        public BoxStorageService(IBoxContentConfiguration boxContentConfiguration)
        {
            _configuration = boxContentConfiguration;

            _client = new MongoClient(_configuration.GetConnectionString());
        }

        public Box Create(Box box)
        {
            // No name
            if (string.IsNullOrWhiteSpace(box.name))
                return null;

            // No owner
            if (string.IsNullOrWhiteSpace(box.ownerID))
                return null;

            var box_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<Box>(_configuration.GetBoxCollectionName());

            box_collection.InsertOne(box);

            return box;
        }

        public Box Delete(Box box)
        {
            var box_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<Box>(_configuration.GetBoxCollectionName());

            FilterDefinition<Box> filter = Builders<Box>.Filter.Eq("boxID", box.boxID);

            var delete_result = box_collection.DeleteOne(filter);

            if (delete_result.DeletedCount == 1)
                return box;

            return null;

        }

        private List<Box> GetByProperty(string property, string value)
        {
            var box_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<Box>(_configuration.GetBoxCollectionName());

            FilterDefinition<Box> filter = Builders<Box>.Filter.Eq(property, value);

            var boxes = box_collection.Find(filter)?.ToList();

            return boxes;
        }

        public Box GetByID(string id)
        {
            return GetByProperty("boxID", id)?.FirstOrDefault();
        }

        public List<Box> GetByUser(string userId)
        {
            return GetByProperty("ownerID", userId);
        }

        public List<Box> Search(BoxSearchSettings settings, User user)
        {
            var box_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<Box>(_configuration.GetBoxCollectionName());

            FilterDefinition<Box> filter = null;

            switch (settings.searchType)
            {
                case BoxSearchType.SearchByName:
                    filter = Builders<Box>.Filter.Eq("name", settings.searchTerm);
                    break;
                case BoxSearchType.SearchByItems:
                    filter = Builders<Box>.Filter.Eq("items", settings.searchTerm);
                    break;
                case BoxSearchType.SearchByAll:
                    filter = Builders<Box>.Filter.And(
                        Builders<Box>.Filter.Eq("name", settings.searchTerm),
                        Builders<Box>.Filter.Eq("items", settings.searchTerm));
                    break;
            }

            var boxes = box_collection.Find(filter)?.ToList();

            return boxes;
        }

        public Box Update(Box box)
        {
            var box_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<Box>(_configuration.GetBoxCollectionName());

            FilterDefinition<Box> filter = Builders<Box>.Filter.Eq("boxID", box.boxID);

            UpdateDefinition<Box> update = Builders<Box>.Update
                .Set(x => x.name, box.name)
                .Set(x => x.items, box.items)
                .Set(x => x.ownerID, box.ownerID)
                .Set(x => x.lastUpdatedAt, DateTime.UtcNow);

            var update_result = box_collection.UpdateOne(filter, update);

            if (update_result.ModifiedCount > 0)
                return GetByID(box.boxID);

            return null;
        }
    }
}
