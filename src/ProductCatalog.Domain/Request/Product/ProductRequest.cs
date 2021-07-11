using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Domain.Request.Product
{
    public abstract class ProductRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}
