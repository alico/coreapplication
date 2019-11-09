using CoreApplication.Common;
using CoreApplication.DTO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CoreApplication.Middlewares
{
    public class ResponseWrapper
    {
        private RequestDelegate next;

        public ResponseWrapper(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await next(context);

            var response = new CommonAPIResponse();

            if (context.Items["exception"] != null)
            {
                var errorResponse = ApiErrorResponseFactory.Create((Exception)context.Items["exception"]);
                errorResponse.CorrelationId = context.Items["correlationId"]?.ToString() ?? string.Empty;

                context.Response.StatusCode = errorResponse.StatusCode;
                response.Result = errorResponse;
            }
            else
            {
                try
                {
                    response.Result = JsonConvert.DeserializeObject<object>(context.Items["responseMessage"].ToString());
                }
                catch (Exception)
                {
                    response.Result = new ApiErrorResponse(500, "Invalid response format");
                    context.Response.StatusCode = 500;
                }
            }

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
