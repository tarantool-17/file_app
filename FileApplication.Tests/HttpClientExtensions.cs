using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileApplication.Tests
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> Status(this Task<HttpResponseMessage> task, HttpStatusCode statusCode)
        {
            var response = await task;
            await response.EnsureStatusCodeAsync(statusCode);
            return response;
        }

        public static async Task<T> Response<T>(this Task<HttpResponseMessage> task, HttpStatusCode? statusCode = null)
        {
            var response = await task;
            if (statusCode.HasValue)
                await response.EnsureStatusCodeAsync(statusCode.Value);
            else
                await response.EnsureSuccessfulAsync();
            return await response.Content.ReadAsAsync<T>();
        }

        public static Task EnsureSuccessfulAsync(this HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode ? Task.CompletedTask : ThrowResponseError(response);
        }
        
        public static Task EnsureStatusCodeAsync(this HttpResponseMessage response, HttpStatusCode code)
        {
            return response.StatusCode == code ? Task.CompletedTask : ThrowResponseError(response);
        }
        
        private static Task ThrowResponseError(HttpResponseMessage response)
        {
            throw new InvalidOperationException($"HTTP {(int)response.StatusCode}: {response.StatusCode}");
        }
    }
}