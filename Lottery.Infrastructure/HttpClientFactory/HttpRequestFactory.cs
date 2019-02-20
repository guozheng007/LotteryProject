using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Infrastructure
{
    public class HttpRequestFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpRequestFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> Get(string requestUri)
            => await Get(requestUri, "");

        public async Task<HttpResponseMessage> Get(string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClientFactory)
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Post(string requestUri, object value)
            => await Post(requestUri, value, "");

        public async Task<HttpResponseMessage> Post(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClientFactory)
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }


        public async Task<HttpResponseMessage> Put(string requestUri, object value)
            => await Put(requestUri, value, "");

        public async Task<HttpResponseMessage> Put(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClientFactory)
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }


        public async Task<HttpResponseMessage> Patch(string requestUri, object value)
            => await Patch(requestUri, value, "");

        public async Task<HttpResponseMessage> Patch(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClientFactory)
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddContent(new PatchContent(value))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }


        public async Task<HttpResponseMessage> Delete(string requestUri)
           => await Delete(requestUri, "");

        public async Task<HttpResponseMessage> Delete(
            string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClientFactory)
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }


        public async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName)
            => await PostFile(requestUri, filePath, apiParamName, "");

        public async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClientFactory)
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new FileContent(filePath, apiParamName))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }
    }
}
