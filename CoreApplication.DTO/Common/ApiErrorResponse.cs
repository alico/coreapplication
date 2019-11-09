using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.DTO
{
    public class ApiErrorResponse
    {
        public string CorrelationId { get; set; }
        public int StatusCode { get; private set; }
        public string Description { get; private set; }

        public ApiErrorResponse(int statusCode, string description)
        {
            this.StatusCode = statusCode;
            this.Description = description;
        }


    }
}
