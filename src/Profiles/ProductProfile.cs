using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Product;
using ProductCatalog.Domain.Response.Product;

namespace ProductCatalog.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductRequest, Product>();
        }
    }
}
