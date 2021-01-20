using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ClinicSchedule.Web
{
    class RequestResponseLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLogMiddleware> _logger;

        public RequestResponseLogMiddleware(RequestDelegate next, ILogger<RequestResponseLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            request.EnableBuffering();

            var response = context.Response;
            Stream originalResponseBody = response.Body;

            using (var responseBodyMemoryStream = new MemoryStream())
            {
                response.Body = responseBodyMemoryStream;

                using (var streamReader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    string requestBodyStr = await streamReader.ReadToEndAsync();
                    request.Body.Position = 0;

                    requestBodyStr = requestBodyStr != String.Empty ? requestBodyStr : "none";
                    _logger.LogInformation("Request body: {requestBodyStr}", requestBodyStr);
                }

                await _next.Invoke(context);

                responseBodyMemoryStream.Position = 0;
                using (var streamReader = new StreamReader(responseBodyMemoryStream, Encoding.UTF8, true, 1024, true))
                {
                    string responseBodyStr = await streamReader.ReadToEndAsync();
                    responseBodyMemoryStream.Position = 0;

                    await responseBodyMemoryStream.CopyToAsync(originalResponseBody);
                    response.Body = originalResponseBody;

                    responseBodyStr = responseBodyStr != String.Empty ? responseBodyStr : "none";
                     _logger.LogInformation("Response body: {responseBodyStr}", responseBodyStr);
                }
            }
        }
    }
}