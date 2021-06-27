using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Domain.Request.Product
{
    public abstract class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}
