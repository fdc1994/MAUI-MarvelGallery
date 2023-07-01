using MarvelGallery.DataModels;
using MarvelGallery.Resources;
using MarvelGallery.Utils;
using Newtonsoft.Json;
using Refit;

namespace MarvelGallery.Services
{
    public class MarvelApiService
    {
        private readonly HttpClient _httpClient;

        private readonly String _privateApiKey = Constants.privateApiKey;
        private readonly String _publicApiKey = Constants.publicApiKey;

        public MarvelApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.baseURL)
            };
        }

        public async Task<MarvelApiResponse> GetCharacters(string nameStartsWith, int? limit = null, int? offset = null)
        {
            String category = "characters?";

            var query = $"{category}{appendAuthentication()}";

            if (nameStartsWith != String.Empty)
            {
                query = query+ Constants.and + "nameStartsWith=" + nameStartsWith ;
            }

            var response = await _httpClient.GetAsync(query);
            string json = await response.Content.ReadAsStringAsync();
            MarvelApiResponse marvelApiResponse = JsonConvert.DeserializeObject<MarvelApiResponse>(json);

       
            return marvelApiResponse;
        }

        private String generateHash(string timeStamp) {
            String stringToHash = timeStamp + _privateApiKey + _publicApiKey;
            return HashUtils.GenerateMD5(stringToHash);          
        }

        private String generateTimeStamp() {
            return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        }    
        private String appendAuthentication()
        {
            String timeStamp = generateTimeStamp();
            String hash = generateHash(timeStamp);

            return $"{"ts=" + timeStamp+ Constants.and + "apikey=" + _publicApiKey + Constants.and + "hash=" + hash}";
        }
    }
}
