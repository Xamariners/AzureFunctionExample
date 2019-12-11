using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;

namespace AzureFunctionExample.Services.Calc.Api.Tests
{
    public static class MockHelpers
    {
        public static Mock<HttpRequestMessage> CreateMockRequestMessage(string payload)
        {
            var mockRequest = new Mock<HttpRequestMessage>();
            HttpContent content = new StringContent(payload);
            mockRequest.Setup(req => req.Content).Returns(content);
            return mockRequest;
        }

        public static HttpRequestMessage CreateRequestMessage(string payload)
        {
            var requestMessage = new HttpRequestMessage();

            HttpContent content = new StringContent(payload);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            requestMessage.Content = content;
            return requestMessage;
        }

        public static Mock<HttpRequest> CreateMockRequest(string payload)
        {
            var mockRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(payload);
            writer.Flush();
            stream.Position = 0;
            mockRequest.Setup(req => req.Body).Returns(stream);
            return mockRequest;
        }

        public static DefaultHttpRequest CreateHttpRequest()
        {
            return new DefaultHttpRequest(new DefaultHttpContext());
        }

        public static HttpRequestMessage CreateRequestMessage()
        {
            return CreateRequestMessage("");
        }

        public static DefaultHttpRequest CreateHttpRequest(Stream stream)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Body = stream
            };
            return request;
        }

        public static HttpRequestMessage CreateRequestMessage(Stream payload)
        {
            var requestMessage = new HttpRequestMessage();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(payload);
            writer.Flush();
            stream.Position = 0;
            HttpContent content = new StreamContent(stream);
            requestMessage.Content = content;
            return requestMessage;
        }

        public static DefaultHttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue))
            };
            return request;
        }

        public static HttpRequestMessage CreateHttpRequestMessage(string queryStringKey, string queryStringValue)
        {
            var request = CreateRequestMessage();

            var query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue)).ToString();
            var uri = new Uri(request.RequestUri.PathAndQuery + query);
            request.RequestUri = uri;

            return request;
        }

        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key, value }
            };
            return qs;
        }

        public static HttpRequestMessage CreateFormDataRequestMessage(string payload)
        {
            var requestMessage = new HttpRequestMessage();

            HttpContent content = new StringContent(payload);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            requestMessage.Content = content;
            return requestMessage;
        }
    }
}
