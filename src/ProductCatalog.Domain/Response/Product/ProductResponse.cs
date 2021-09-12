using System;

namespace ProductCatalog.Domain.Response.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}
