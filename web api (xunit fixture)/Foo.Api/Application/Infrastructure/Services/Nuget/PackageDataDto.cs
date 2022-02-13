using System.Collections.Generic;

namespace Foo.Api.Application.Infrastructure.Services.Nuget
{
    public class PackageDataDto
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public int TotalDownloads { get; set; }
        public List<VersionsDto> Versions { get; set; } = new List<VersionsDto>();
    }
}
