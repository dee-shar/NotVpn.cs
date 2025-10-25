using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace NotVpnApi
{
    public class NotVpn
    {
        private readonly HttpClient httpClient;
        private readonly string deviceId;
        private readonly string apiUrl = "https://api.notvpn.io";
        private readonly string appVersion = "85";
        private readonly string operationSystem = "android";
        public NotVpn()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("okhttp/4.9.2");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            deviceId = GenerateDeviceId(16);
        }
        
        private static string GenerateDeviceId(int length)
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            using var randomNumbers = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            randomNumbers.GetBytes(bytes);
            return new string(bytes.Select(b => characters[b % characters.Length]).ToArray());
        }

        public async Task<string> Login(string token)
        {
            var content = new StringContent(
                $"token={token}&v={appVersion}&os={operationSystem}&versionOs=28&deviceName=RMX3551&deviceName=RMX3551", Encoding.UTF8);
            var response = await httpClient.PostAsync($"{apiUrl}/users/autorisation", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Register()
        {
            var data =
                $"mail=false&language=ru&languageOriginal=ru&languageOriginalTeg=ru-RU&countryUser=ru&json={{\"full\":false,\"elements\":[\"instagram\",\"facebook\",\"twitter\",\"youtubeimage\"]}}&os={operationSystem}&v={appVersion}&litel=true&deviceId={deviceId}";
            var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync($"{apiUrl}/users/registration", content);
            return await response.Content.ReadAsStringAsync();
        }
        
        public async Task<string> GetServers(string token)
        {
            var countries = "[\"DE\",\"CA\",\"FI\",\"US\",\"NL\",\"SG\",\"RU\",\"UA\",\"BY\",\"TR\",\"AU\",\"KZ\",\"ES\",\"PL\",\"FR\",\"SE\",\"CH\",\"EE\",\"NO\",\"BG\",\"RO\",\"DK\",\"CZ\",\"GB\"]";
            var data = $"token={token}&counrty_list={countries}&rate=true&v={appVersion}";
            var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync($"{apiUrl}/ping/get_servers", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
