using QRViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRViewer.Repositories
{
    public interface IUrlDataRepository
    {
        Task<IEnumerable<DataUrl>> GetUrlsAsync();
    }
}
