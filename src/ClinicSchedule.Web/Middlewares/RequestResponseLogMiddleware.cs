using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace ClinicSchedule.Web
{
    class RequestResponseLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            DateTime requestTime = DateTime.Now;
            DateTime responseTime;

            string requestBodyStr = "";
            string responseBodyStr = "";
            
            var request = context.Request;
            request.EnableBuffering();

            var response = context.Response;
            Stream originalResponseBody = response.Body;

            using (var responseBodyMemoryStream = new MemoryStream())
            {
                response.Body = responseBodyMemoryStream;

                using (var streamReader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    requestBodyStr = await streamReader.ReadToEndAsync();
                    request.Body.Position = 0;
                }

                await _next.Invoke(context);

                responseBodyMemoryStream.Position = 0;
                using (var streamReader = new StreamReader(responseBodyMemoryStream, Encoding.UTF8, true, 1024, true))
                {
                    responseBodyStr = await streamReader.ReadToEndAsync();
                    responseBodyMemoryStream.Position = 0;
                    await responseBodyMemoryStream.CopyToAsync(originalResponseBody);
                    response.Body = originalResponseBody;
                }

                responseTime = DateTime.Now;
            }

            // Console.WriteLine($"Time: {requestTime:HH:mm:ss:ffffff}; " + 
            //         $"Request: Scheme={request.Scheme}, Host={request.Host}, Path={request.Path}, QueryString={request.QueryString}, Body={requestBodyStr}");

            // Console.WriteLine($"Time: {responseTime:HH:mm:ss:ffffff}; " +
            //         $"Response: Status={response.StatusCode}, Body={responseBodyStr}; Request-Response Duration: {responseTime - requestTime}");
            
        }
    }
}