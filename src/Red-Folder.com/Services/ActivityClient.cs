using Newtonsoft.Json;
using Red_Folder.com.Models.Activity;
using System.Net.Http;
using System.Threading.Tasks;

namespace Red_Folder.com.Services
{
    public class ActivityClient
    {
        private static HttpClient _httpClient = new HttpClient();
        private string _activityUrl;
        private string _activityCode;

        public ActivityClient(string activityUrl, string activityCode)
        {
            _activityUrl = activityUrl;
            _activityCode = activityCode;
        }

        public async Task<Weekly> Weekly(string year, string weekNumber)
        {
            var url = $"{_activityUrl}/weeklyactivity/{year}/{weekNumber}?code={_activityCode}";
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
