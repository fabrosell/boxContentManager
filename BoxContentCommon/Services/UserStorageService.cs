using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using BoxContentCommon.Entities;
using BoxContentCommon.Interfaces;

namespace BoxContentCommon.Services
{
    public class UserStorageService : IUserStorageService
    {
        private readonly IBoxContentConfiguration _configuration;
        private readonly MongoClient _client;

        public UserStorageService(IBoxContentConfiguration boxContentConfiguration)
        {
            _configuration = boxContentConfiguration;

            _client = new MongoClient(_configuration.GetConnectionString());
        }

        public User Create(User user)
        {
            // No email or name
            if (string.IsNullOrWhiteSpace(user.eMail))
                return null;

            if (string.IsNullOrWhiteSpace(user.name))
                return null;

            var user_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<User>(_configuration.GetUserCollectionName());

            user_collection.InsertOne(user);

            return user;
        }

        public User Delete(User user)
        {
            var user_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<User>(_configuration.GetUserCollectionName());

            FilterDefinition<User> filter = Builders<User>.Filter.Eq("userID", user.userID);

            var delete_result = user_collection.DeleteOne(filter);

            if (delete_result.DeletedCount == 1)
                return user;

            return null;
        }

        private User GetByProperty(string property, string value)
        {
            var user_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<User>(_configuration.GetUserCollectionName());

            FilterDefinition<User> filter = Builders<User>.Filter.Eq(property, value);

            var user = user_collection.Find(filter).FirstOrDefault();

            return user;
        }

        public User GetByEmail(string email)
        {
            return GetByProperty("eMail", email);
        }

        public User GetByID(string id)
        {
            return GetByProperty("userID", id);
        }

        public User Update(User user)
        {
            var user_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<User>(_configuration.GetUserCollectionName());

            FilterDefinition<User> filter = Builders<User>.Filter.Eq("userID", user.userID);

            UpdateDefinition<User> update = Builders<User>.Update
                .Set(x => x.eMail, user.eMail)
                .Set(x => x.name, user.name)
                .Set(x => x.lastUpdatedAt, DateTime.UtcNow);

            var update_result = user_collection.UpdateOne(filter, update);

            if (update_result.ModifiedCount > 0)
                return GetByID(user.userID);

            return null;
        }

        // Bonus track: async version of the update method
        public async Task<UpdateResult> UpdateAsync(User user)
        {
            var user_collection = _client.GetDatabase(_configuration.GetDatabaseName()).GetCollection<User>(_configuration.GetUserCollectionName());

            FilterDefinition<User> filter = Builders<User>.Filter.Eq("userID", user.userID);

            UpdateDefinition<User> update = Builders<User>.Update
                .Set(x => x.eMail, user.eMail)
                .Set(x => x.name, user.name)
                .Set(x => x.lastUpdatedAt, DateTime.UtcNow);

            return await user_collection.UpdateOneAsync(filter, update);
        }
    }
}
