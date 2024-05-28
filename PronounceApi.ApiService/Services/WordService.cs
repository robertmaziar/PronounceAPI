using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PronounceApi.ApiService.Models;

namespace PronounceApi.ApiService.Services
{
    public class WordsService
    {
        private readonly IMongoCollection<Word> _wordsCollection;

        public WordsService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(
                dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.DatabaseName);

            _wordsCollection = mongoDatabase.GetCollection<Word>(
                dbSettings.Value.WordsCollectionName);
        }

        public async Task<List<Word>> GetAsync() =>
            await _wordsCollection.Find(_ => true).ToListAsync();

        public async Task<Word?> GetAsync(string baseWord) =>
            await _wordsCollection.Find(x => x.BaseWord == baseWord).FirstOrDefaultAsync();

        public async Task CreateAsync(Word newWord) =>
            await _wordsCollection.InsertOneAsync(newWord);

        public async Task UpdateAsync(string id, Word updatedWord) =>
            await _wordsCollection.ReplaceOneAsync(x => x.Id == id, updatedWord);

        public async Task RemoveAsync(string id) =>
            await _wordsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
