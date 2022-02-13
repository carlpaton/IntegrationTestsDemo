using Foo.Api.Application.Infrastructure.Common;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Foo.Api.Application.Infrastructure.Services.Nuget
{
    public class NugetServiceClient : INugetServiceClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<NugetServiceClient> _logger;
        private readonly NugetServiceOptions _options;

        public NugetServiceClient(HttpClient client, ILogger<NugetServiceClient> logger,
            IOptions<NugetServiceOptions> options)
        {
            _options = options.Value;
            _logger = logger;

            client.BaseAddress = new Uri(_options.Url);
            _client = client;
        }

        public async Task<NugetServiceQueryResponse> QueryPackageAsync(string packageName, string version)
        {
            _logger.LogInformation($"QueryPackage packageName={packageName}");

            var queryString = new Dictionary<string, string>
            {
                { "q", packageName },
                { "take", _options.MaxReceive }
            };

            var requestUri = QueryHelpers.AddQueryString("/query", queryString);
            var response = await _client.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();

            var nugetPackage = JsonSerializer.Deserialize<NugetPackageDto>(content, SerializationOptions.Deserialize);

            // nothing found, example search on `lkjnsdkyfgislkdmflsdjfsklfn`
            var data = nugetPackage.Data.FirstOrDefault();
            if (data == null) 
            {
                return new NugetServiceQueryNotFoundResponse();
            }

            // check the package name is correct, nuget api pops it at the top of the results so take=1 should give us the right result
            if (!data.Id.Equals(packageName, StringComparison.CurrentCultureIgnoreCase)) {
                return new NugetServiceQueryNotFoundResponse();
            }

            // if the searched upon `version` is an older version then the query is still valid
            // the current version is both in `data.verion` and `data.versions[x].version`
            var versions = data.Versions;
            if (versions.Any(x => x.Version == version)) {
                var versionDto = versions
                    .Where(x => x.Version == version)
                    .FirstOrDefault();

                // TODO these values should be in appsettings
                var description = data.Description;
                if (description.Length > 500) { 
                    description = description[..497];
                    description += "...";
                }

                return new NugetServiceQueryResponse()
                {
                    Description = description,
                    Name = data.Id,
                    Downloads = versionDto.Downloads,
                    Version = versionDto.Version
                };
            }

            // package name matched example `foobar` but the version was not found
            return new NugetServiceQueryNotFoundResponse();
        }
    }
}
