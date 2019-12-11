using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AzureFunctionExample.Services.Calc.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Performs HttpRequest body parsing and throws if parsing fails.
        /// </summary>
        /// <typeparam name="T">Type to convert body to</typeparam>
        /// <param name="request">Instance of HttpRequest</param>
        /// <returns>Instance of T</returns>
        public static async Task<T> ParseBodyAndThrow<T>(this HttpRequest request)
        {
            try
            {
                string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<T>(requestBody);
                if (data == null)
                {
                    throw new ArgumentNullException("Body", "Empty payload found.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new FormatException("Error parsing payload.", ex);
            }
        }

        /// <summary>
        /// Performs HttpRequest body parsing and throws if parsing fails.
        /// </summary>
        /// <typeparam name="T">Type to convert body to</typeparam>
        /// <param name="request">Instance of HttpRequest</param>
        /// <returns>Instance of T</returns>
        public static async Task<T> ParseContentAndThrow<T>(this HttpRequestMessage request)
        {
            try
            {
                T data = await request.Content.ReadAsAsync<T>();
                if (data == null)
                {
                    throw new ArgumentNullException("Body", "Empty payload found.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new FormatException("Error parsing payload.", ex);
            }
        }

        /// <summary>
        /// Tries to parse the HttpRequest body. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <typeparam name="T">Type to convert body to</typeparam>
        /// <param name="request">Instance of HttpRequest</param>
        /// <returns>Tuple. bool to indicate success or fail. Instance of T</returns>
        public static async Task<(bool succeeded, T body)> TryParseContent<T>(this HttpRequestMessage request)
        {
            try
            {
                T data = await request.Content.ReadAsAsync<T>();
                return (succeeded: data != null, body: data);
            }
            catch
            {
                return (succeeded: false, body: default(T));
            }
        }
    }
}
