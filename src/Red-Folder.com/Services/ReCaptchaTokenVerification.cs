using Newtonsoft.Json;
using RedFolder.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public class ReCaptchaTokenVerification : ITokenVerification
    {
        private const string GOOGLE_URL = "https://www.google.com/recaptcha/api/siteverify";
        private readonly HttpClient _httpClient;
        private readonly ReCaptchaConfiguration _configuration;

        public ReCaptchaTokenVerification(HttpClient httpClient, ReCaptchaConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> Valid(string token)
        {
            try
            {
                var url = $"{GOOGLE_URL}?secret={_configuration.SecretKey}&response={token}";

                var httpResponse = await _httpClient.PostAsync(url, null);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseJson = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Response>(responseJson);

                    return response.Success && response.Score > 0.5;
                }
            }
            catch
            {
            }

            return false;
        }

        private class Response
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
            [JsonProperty("score")]
            public float Score { get; set; }
            [JsonProperty("action")]
            public string Action { get; set; }
            [JsonProperty("challenge_ts")]
            public DateTime ChallengeTimestamp { get; set; }
            [JsonProperty("hostname")]
            public string Hostname { get; set; }
            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}
