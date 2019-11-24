using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QRViewer.Models;
using QRViewer.Repositories;
using QRViewer.Services;

namespace QRViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IChartApiService chartApiService;
        private readonly IUrlDataRepository urlDataRepository;
        public QRController(IConfiguration _configuration, IChartApiService _chartApiService, IUrlDataRepository _urlDataRepository)
        {
            configuration = _configuration;
            chartApiService = _chartApiService;
            urlDataRepository = _urlDataRepository;
        }

        // GET api/qr   
        [HttpGet]
        public async Task<IEnumerable<DataUrl>> Get()
        {            
            return await urlDataRepository.GetUrlsAsync();
        }

        // GET api/qr/url
        [HttpGet("{url}")]
        public async Task<IActionResult> GetAsync(string url)
        {
            try
            {
                var urlDecode =Encoding.UTF8.GetString(Convert.FromBase64String(url));
                var response = await chartApiService.GetAsync($"{configuration["ChartApi:Url"]}{urlDecode}");
                return File(response, "image/png");
            }
            catch (Exception ex)
            {
                //log exception
                return this.StatusCode(StatusCodes.Status500InternalServerError, "api error");
            }
        }
    }
}
