using App.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Banck.Api.Helpers
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly RecyclableMemoryStreamManager streamManager;

        public LoggerMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            streamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsSwagger(context) || context.Request.Method == HttpMethods.Options)
            {
                await next(context);
            }
            else
            {
                var start = Stopwatch.GetTimestamp();
                await LogRequest(context);

                var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
                await LogResponse(context, elapsedMs);
            }
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");
        }

        public double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = streamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            Log.Information($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");

            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context, double elapsedMs)
        {

            var originalBodyStream = context.Response.Body;
            await using var responseBody = streamManager.GetStream();
            context.Response.Body = responseBody;
            await next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var userInformation = (UserInformation)context.Items["UserInformation"];
            String jsonStringAuthorizeUser = JsonConvert.SerializeObject(userInformation, Formatting.Indented);

            Log.Information($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"TimeElapsed: {elapsedMs:0.0000} " +
                                   $"Response Body: {text}" +
                                   $"User Information:{jsonStringAuthorizeUser}");

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
