using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace PronounceApi.ApiService.Models
{
    public class Word
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }

        public string BaseWord { get; set; } = null!;
        public string HyphenatedWord { get; set; } = null!;
        public string StressPattern { get; set; } = null!;
        public int SyllableCount { get; set; }
    }
}
