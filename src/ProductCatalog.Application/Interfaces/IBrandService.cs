using ProductCatalog.Domain;
using ProductCatalog.Domain.BaseResponse;
using ProductCatalog.Domain.ErrorsResponse;
using ProductCatalog.Domain.Request.Brand;
using ProductCatalog.Domain.Response;
using ProductCatalog.Domain.Response.Brand;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service.Abstraction
{
    public interface IBrandService
    {
        Task<CreateBrandResponse> AddAsync(CreateBrandRequest brand);
        Task<GetBrandResponse> GetBrandByIdAsync(Guid id);
        Task<IEnumerable<GetBrandResponse>> GetAllBrandAsync();
        Task DeleteBrandAsync(Guid id);
        Task<UpdateBrandResponse> UpdateBrandAsync(Guid id, UpdateBrandRequest obj);
    }
}
