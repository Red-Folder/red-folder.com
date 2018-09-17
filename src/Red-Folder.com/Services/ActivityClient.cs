using Newtonsoft.Json;
using Red_Folder.com.Models.Activity;
using System.Collections.Generic;
using System.Net.Http;

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

        public Weekly Weekly(int year, int weekNumber)
        {
            var url = $"{_activityUrl}/weeklyactivity/{year}/{weekNumber}?code={_activityCode}";
            var rawResponse = _httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<Weekly>(rawResponse);
        }
    }
}
