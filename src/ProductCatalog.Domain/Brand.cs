using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ProductCatalog.Domain
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}
