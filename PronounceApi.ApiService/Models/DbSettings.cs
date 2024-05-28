namespace PronounceApi.ApiService.Models
{
    public class DbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string WordsCollectionName { get; set; } = null!;
    }
}
