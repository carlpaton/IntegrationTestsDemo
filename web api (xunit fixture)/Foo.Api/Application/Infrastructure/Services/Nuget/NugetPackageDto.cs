using System.Collections.Generic;

namespace Foo.Api.Application.Infrastructure.Services.Nuget
{
    public class NugetPackageDto
    {
        public List<PackageDataDto> Data { get; set; } = new List<PackageDataDto>();
    }
}
