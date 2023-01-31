using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoExample.Models;

namespace MongoExample.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Systems> _playlistCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new (mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<Systems>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Systems>> GetAsync()
        {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Systems>> GetFilterAsync(string column, string value)
        {
            var filter = Builders<Systems>.Filter.Eq(column, value);

            return await _playlistCollection.Find(filter).ToListAsync();

        }
        public async Task<Systems> GetFilterUniqueAsync(string value)
        {
            var filter = Builders<Systems>.Filter.Eq("_id", value);

            return await _playlistCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Systems playlist)
        {
            await _playlistCollection.InsertOneAsync(playlist);
            return;
        }
        public async Task AddToPlaylistAsync(string id, string movieId)
        {
            FilterDefinition<Systems> filter = Builders<Systems>.Filter.Eq("ClientId", id);
            UpdateDefinition<Systems> update = Builders<Systems>.Update.AddToSet<string>("movieIds", movieId);
            await _playlistCollection.UpdateOneAsync(filter, update);
            return;
        }
        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Systems> filter = Builders<Systems>.Filter.Eq("ClientId", id);
            await _playlistCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
