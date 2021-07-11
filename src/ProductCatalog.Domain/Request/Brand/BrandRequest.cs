using Newtonsoft.Json;

namespace ProductCatalog.Domain.Request.Brand
{
    public class BrandRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
