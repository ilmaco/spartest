using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QRViewer.Services
{
    public class ApiProxyFactory : IApiProxyFactory
    {
        private readonly IConfiguration configuration;
        public ApiProxyFactory(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IWebProxy GetWebProxy()
        {
            WebProxy proxy = null;
            if (configuration.GetValue<bool>("ChartApi:Proxy:UseProxy"))
            {

                proxy = new WebProxy
                {
                    Address = new Uri($"http://{configuration.GetValue<string>("ChartApi:Proxy:Host")}:{configuration.GetValue<int>("ChartApi:Proxy:Port")}"),
                    BypassProxyOnLocal = configuration.GetValue<bool>("ChartApi:Proxy:BypassOnLocal"),
                    UseDefaultCredentials = configuration.GetValue<bool>("ChartApi:Proxy:UseDefaultCredential")
                };

                if (!configuration.GetValue<bool>("ChartApi:Proxy:UseDefaultCredential"))
                {
                    proxy.Credentials = new NetworkCredential(
                                                 userName: configuration.GetValue<string>("ChartApi:Proxy:UserName"),
                                                 password: configuration.GetValue<string>("ChartApi:Proxy:Password")
                                         );
                }

            }
            return proxy;
        }
    }
}
