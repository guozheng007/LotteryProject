using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Infrastructure
{
    public class HttpRequestBuilder
    {
        private HttpMethod method = null;
        private string requestUri = string.Empty;
        private HttpContent content = null;
        private string bearerToken = string.Empty;
        private string customToken = string.Empty;
        private string customTokenName = string.Empty;
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(0, 0, 15);
        private readonly IHttpClientFactory _httpClientFactory;


        public HttpRequestBuilder(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }


        public HttpRequestBuilder()
        {

        }


        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }


        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }


        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }


        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }


        public HttpRequestBuilder AddCustomToken(string customTokenName, string customToken)
        {
            this.customToken = customToken;
            this.customTokenName = customTokenName;
            return this;
        }


        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }


        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }


        public async Task<HttpResponseMessage> SendAsync()
        {
            CheckRequiredArguments();

            var request = CreateHttpRequestMessage();
            var client = CreateHttpClientFactory();

            return await client.SendAsync(request);
        }


        private HttpClient CreateHttpClientFactory()
        {
            var client = this._httpClientFactory.CreateClient();
            client.Timeout = this.timeout;

            return client;
        }


        private HttpRequestMessage CreateHttpRequestMessage()
        {
            var request = new HttpRequestMessage
            {
                Method = this.method,
                RequestUri = new Uri(this.requestUri)
            };

            CheckHttpRequestMessageHeadersParamters(request);

            return request;
        }


        private void CheckHttpRequestMessageHeadersParamters(HttpRequestMessage request)
        {
            if (this.content != null)
            {
                request.Content = this.content;
            }


            if (!string.IsNullOrEmpty(this.bearerToken))
            {
                request.Headers.Authorization =
                  new AuthenticationHeaderValue("Bearer", this.bearerToken);
            }


            if (!string.IsNullOrEmpty(this.customToken))
            {
                request.Headers.Authorization =
                  new AuthenticationHeaderValue(customTokenName, this.customToken);
            }

            request.Headers.Accept.Clear();

            if (!string.IsNullOrEmpty(this.acceptHeader))
            {
                request.Headers.Accept.Add(
                   new MediaTypeWithQualityHeaderValue(this.acceptHeader));
            }
        }


        private void CheckRequiredArguments()
        {
            if (this.method == null)
                throw new ArgumentNullException("Method");

            if (string.IsNullOrEmpty(this.requestUri))
                throw new ArgumentNullException("Request Uri");
        }
    }
}
