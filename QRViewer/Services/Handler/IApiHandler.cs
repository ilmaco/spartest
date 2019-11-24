using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QRViewer.Services
{
    public interface IApiHandler
    {
        HttpMessageHandler GetHandler();
    }
}
