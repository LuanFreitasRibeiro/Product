using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Product;

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
