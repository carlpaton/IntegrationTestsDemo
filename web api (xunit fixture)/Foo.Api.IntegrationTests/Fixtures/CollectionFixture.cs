using Foo.Api.IntegrationTests.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Foo.Api.IntegrationTests.Fixtures
{
    public class CollectionFixture : IAsyncLifetime
    {
        public CollectionFixture()
        {
            var appSettings = "appsettings.Mock.json";

            if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Uat")
                appSettings = "appsettings.Uat.json";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(appSettings)
                .Build();

            _fooBar = configuration
                .GetSection(FooBarOptions.FooBar)
                .Get<List<FooBarOptions>>();
        }

        public FooServiceClient FooServiceClient
        {
            get
            {
                if (_client != null)
                    return _client;

                _client = new FooServiceClient(_fooBar);
                return _client;
            }
        }

        public Task DisposeAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task InitializeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
