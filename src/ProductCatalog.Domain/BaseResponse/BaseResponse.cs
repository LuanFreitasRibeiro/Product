﻿using System;
using System.Net;

namespace ProductCatalog.Domain.BaseResponse
{
    public class BaseResponse<TSuccess, TError> : BaseResponse<TSuccess>
    {
        public TError Error { get; set; }
    }

    public class BaseResponse<TSuccess>
    {
        public TSuccess Data { get; set; }

        public long ElapsedTime { get; set; }

        public Exception Exception { get; set; }

        public string RawRequest { get; set; }

        public string RawResponse { get; set; }

        public HttpStatusCode? StatusCode { get; set; }

        public bool IsSuccessfully
        {
            get
            {
                return (this.Is2XX && this.Exception == null);
            }
        }

        public bool Is2XX
        {
            get
            {
                if (StatusCode == null)
                {
                    return false;
                }

                int statusCode = (int)this.StatusCode.Value;

                return (statusCode >= 200 && statusCode < 300);
            }
        }
    }
}
