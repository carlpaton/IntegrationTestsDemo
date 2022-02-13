using Foo.Api.Application.Infrastructure.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Foo.Api.Application.Infrastructure.Services.OssIndex
{
    public class OssIndexClient : IOssIndexClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<OssIndexClient> _logger;

        public OssIndexClient(HttpClient client, ILogger<OssIndexClient> logger,
            IOptions<OssIndexOptions> options)
        {
            _logger = logger;

            client.BaseAddress = new Uri(options.Value.Url);
            _client = client;
        }

        public async Task<IEnumerable<OssIndexResponse>> GetComponentReport(string coordinates)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"component-report/{coordinates}");
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode) 
            {
                // TODO this doesnt tell the consumer about 409's. If we cannot get the data the call should fail before it persists to the db
                return new List<OssIndexResponse>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var ossIndexDto = JsonSerializer.Deserialize<OssIndexDto>(content, SerializationOptions.Deserialize);

            // TODO validations / checks

            var ossIndexResponse = new List<OssIndexResponse>();
            foreach (var vulnerability in ossIndexDto.Vulnerabilities)
            {
                ossIndexResponse.Add(new OssIndexResponse()
                {
                    CvssScore = vulnerability.CvssScore.ToString(),
                    Description = vulnerability.Description,
                    Reference = vulnerability.Reference,
                    Title = vulnerability.Title
                });
            }

            return ossIndexResponse;
        }
    }
}
