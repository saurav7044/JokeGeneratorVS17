using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace JokeCompany.Utilities.Http
{
    /// <summary>
    /// Some extensions methods to make a code easy and don't have dublication
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        public static HttpClient Build(this HttpClient httpClient, Uri baseUri)
        {
            var baseAddress = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            httpClient.BaseAddress = baseAddress;
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            return httpClient;
        }

        public static T[] GetAsArray<T>(this HttpClient httpClient, string query)
        {
            var response = httpClient.GetStringAsync(query).GetAwaiter().GetResult();
            return
                response.StartsWith("[")
                    ? JsonConvert.DeserializeObject<T[]>(response)
                    : new T[] {JsonConvert.DeserializeObject<T>(response)};
        }

        public static T[] GetAsArray<T>(this HttpClient httpClient, Uri uri)
        {
            var response = httpClient.GetStringAsync(uri).GetAwaiter().GetResult();
            return
                response.StartsWith("[")
                    ? JsonConvert.DeserializeObject<T[]>(response)
                    : new T[] { JsonConvert.DeserializeObject<T>(response) };
        }
    }
}