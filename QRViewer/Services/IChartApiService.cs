using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRViewer.Services
{
    public interface IChartApiService
    {
        Task<byte[]> GetAsync(string url);
    }
}
