using ProductCatalog.Domain.BaseResponse;
using ProductCatalog.Domain.ErrorsResponse;
using System.Linq;

namespace ProductCatalog.Application
{
    public static class ResponseResultExtensions
    {
        public static string GetError<TSuccess>(this BaseResponse<TSuccess, ErrorsResponse> response)
    where TSuccess : class
        {
            var errors = response?.Error?.Errors;

            if (errors?.Any() == true)
            {
                var error = errors.First();
                return $"{(!string.IsNullOrEmpty(error.Property) ? $"{error.Property}: " : "")}{error.Message}";
            }

            if (!string.IsNullOrWhiteSpace(response?.RawResponse))
            {
                return response?.RawResponse.Trim('"');
            }

            return response?.Exception?.Message;
        }
    }
}
