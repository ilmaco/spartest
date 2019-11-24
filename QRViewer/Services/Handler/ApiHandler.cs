using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace QRViewer.Services
{
    public class ApiHandler : IApiHandler
    {
        private readonly IConfiguration configuration;
        private readonly IApiProxyFactory webProxy;
        public ApiHandler(IConfiguration _configuration, IApiProxyFactory _webProxy)
        {
            configuration = _configuration;
            webProxy = _webProxy;
        }
        public HttpMessageHandler GetHandler()
        {
            return new HttpClientHandler
            {
                Proxy = webProxy.GetWebProxy()
            };
        }
    }
}
