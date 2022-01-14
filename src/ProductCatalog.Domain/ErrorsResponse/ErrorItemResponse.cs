using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Domain.ErrorsResponse
{
    public class ErrorItemResponse
    {
        public ErrorItemResponse() { }

        public ErrorItemResponse(string message)
        {
            this.Message = message;
        }

        public ErrorItemResponse(string message, string property)
        {
            this.Message = message;
            this.Property = property;
        }

        public string Message { get; set; }

        public string Property { get; set; }
    }
}
