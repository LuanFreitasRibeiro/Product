using Newtonsoft.Json;

namespace ProductCatalog.Domain.Request.Category
{
    public class CategoryRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }   
    }
}
