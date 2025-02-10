using Newtonsoft.Json;
using Red_Folder.com.Models.Activity;
using RedFolder.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Red_Folder.com.Services
{
    public class ActivityClient
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly ActivityConfiguration _configuration; 

        // TODO - pass in the IHttpClientFactory
        public ActivityClient(ActivityConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Weekly> Weekly(string year, string weekNumber)
        {
            var url = $"{_configuration.ActivityUrl}/weeklyactivity/{year}/{weekNumber}?code={_configuration.ActivityCode}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Weekly>(json);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}
