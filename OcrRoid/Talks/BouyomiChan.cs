using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OcrRoid.Talks
{
    internal class BouyomiChan : ITalk
    {
        public async Task SpeakAsync(string sentence)
        {
            using var client = new HttpClient();
            var parameters = new Dictionary<string, string>()
            {
                {"text", sentence}
            };

            await this.StopAsync();
            await client.GetAsync(await CreateUriAsync(parameters));
        }
        
        public async Task PauseAsync()
        {
            using var client = new HttpClient();
            await client.GetAsync(CombineBaseUri("pause"));
        }

        public async Task RestartAsync()
        {
            using var client = new HttpClient();
            await client.GetAsync(CombineBaseUri("resume"));
        }

        public async Task StopAsync()
        {
            using var client = new HttpClient();
            await client.GetAsync(CombineBaseUri("clear"));
            await this.RestartAsync();
            await client.GetAsync(CombineBaseUri("skip"));
        }

        private static async Task<string> CreateUriAsync(Dictionary<string, string> parameters)
        {
            return CombineBaseUri($@"talk?{await new FormUrlEncodedContent(parameters).ReadAsStringAsync()}");
        }

        private static string CombineBaseUri(string uri)
            => $@"http://localhost:50080/{uri}";
    }
}
