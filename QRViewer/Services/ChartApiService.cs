using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QRViewer.Services
{
    public class ChartApiService : IChartApiService
    {
        private readonly IApiHandler apiHandler;
        private readonly HttpClient httpClient;
        public ChartApiService(IApiHandler _apihandler)
        {
            apiHandler = _apihandler;
            httpClient = new HttpClient(apiHandler.GetHandler());
        }

        public async Task<byte[]> GetAsync(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
