using ProductCatalog.Domain.Request.User;
using ProductCatalog.Domain.Response.User;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<dynamic> Authenticate(AuthenticateRequest authenticate);
    }
}
