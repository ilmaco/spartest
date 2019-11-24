using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QRViewer.Models;

namespace QRViewer.Repositories
{
    public class JsonUrDataRepository : IUrlDataRepository
    {
        private readonly string Path = @"Data\urls.json";
        public async Task<IEnumerable<DataUrl>> GetUrlsAsync()
        {
            IEnumerable<DataUrl> data = null;
            using (var reader = System.IO.File.OpenText(Path))
            {
                var fileText = await reader.ReadToEndAsync();
                data = JsonConvert.DeserializeObject<IEnumerable<DataUrl>>(fileText);
            }
            return data;
        }
    }
}
