using Foo.Api.IntegrationTests.Infrastructure.Common;
using System;
using System.Net.Http;

namespace Foo.Api.IntegrationTests.Infrastructure.Services
{
    public sealed class FooServiceClient : IDisposable
    {
        private readonly HttpClient _client;

        public FooServiceClient(FooServiceOptions options)
        {
            _client = new HttpClient { BaseAddress = new Uri(options.Url) };
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
