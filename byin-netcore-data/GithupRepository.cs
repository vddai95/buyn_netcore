using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using byin_netcore_transver;
using Microsoft.Extensions.Options;

namespace byin_netcore_data
{
    class GithupRepository : IGitHupRepository
    {
        private readonly AppSettings AppSettings;
        private static readonly HttpClient client = new HttpClient();
        public GithupRepository(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

        public async Task<List<Repository>> GetAllRepository()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var streamTask = client.GetStreamAsync(AppSettings.GithupEndpoint);
            HttpResponseMessage result = client.GetAsync(AppSettings.GithupEndpoint).Result;
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            return repositories;
        }
    }
}
