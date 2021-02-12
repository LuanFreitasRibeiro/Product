using System;
using System.Collections.Generic;

namespace ProductCatalog.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
