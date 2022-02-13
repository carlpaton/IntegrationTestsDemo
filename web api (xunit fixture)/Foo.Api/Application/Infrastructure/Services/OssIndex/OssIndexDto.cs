using System.Collections.Generic;

namespace Foo.Api.Application.Infrastructure.Services.OssIndex
{
    public class OssIndexDto
    {
        public string Coordinates { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public IEnumerable<VulnerabilityDto> Vulnerabilities { get; set; }
    }

    public class VulnerabilityDto
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double CvssScore { get; set; }
        public string CvssVector { get; set; }
        public string Cve { get; set; }
        public string Reference { get; set; }
        public IEnumerable<string> ExternalReferences { get; set; }
    }
}
