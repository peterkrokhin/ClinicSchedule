using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            DateTime requestTime;
            DateTime responseTime;

            requestTime = DateTime.Now;
            Console.WriteLine($"Time: {requestTime:HH:mm:ss:ffffff}, " + 
                $"Request: Scheme={context.Request.Scheme} Host={context.Request.Host} Path={context.Request.Path} QueryString={context.Request.QueryString}");
            
            await _next.Invoke(context);

            responseTime = DateTime.Now;
            Console.WriteLine($"Time: {responseTime:HH:mm:ss:ffffff}, " +
                $"Response: Status={context.Response.StatusCode}, Duration: {responseTime - requestTime}");
        }
    }
}