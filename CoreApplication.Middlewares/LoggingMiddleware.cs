using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CoreApplication.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        private string requestMessage;
        private string responseMessage;
        private ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            requestMessage = string.Empty;
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context.Request);
            await LogResponse(context);
        }

        public async Task LogRequest(HttpRequest request)
        {
            if (request.ContentType == "application/json")
                using (var bodyReader = new StreamReader(request.Body))
                {
                    var body = await bodyReader.ReadToEndAsync();

                    if (request.Method == "GET" || request.Method == "DELETE")
                    {
                        requestMessage = request.QueryString.ToString();
                    }
                    else
                    {
                        request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                        requestMessage = body;
                    }
                }
        }

        public async Task LogResponse(HttpContext context)
        {
            using (var buffer = new MemoryStream())
            {
                var stream = context.Response.Body;
                context.Response.Body = buffer;

                await next.Invoke(context);

                buffer.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(buffer);

                using (var bufferReader = new StreamReader(buffer))
                {
                    string body = await bufferReader.ReadToEndAsync();
                    buffer.Seek(0, SeekOrigin.Begin);
                    context.Response.Body = stream;
                    responseMessage = body;
                    context.Items.Add("responseMessage", responseMessage);

                    var parameters = string.Format("Request : {0}  Response:{1}", requestMessage, responseMessage).ToString();

                    if (context.Items["exception"] != null)
                    {
                        var exception = (Exception)context.Items["exception"];
                        var errorId = context.Items["correlationId"].ToString();

                        //TODO: Async LogAPI call
                        _logger.LogError(exception, "{ErrorId}{Parameters}", errorId, parameters);
                    }
                    else
                    {
                        //TODO: Enable/Disable Logging from configuration and Async LogAPI call
                        //_logger.LogInformation("{Parameters}", parameters);
                    }
                }
            }
        }

    }
}
