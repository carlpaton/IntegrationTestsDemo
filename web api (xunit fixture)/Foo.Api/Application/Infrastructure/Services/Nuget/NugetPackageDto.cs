using System.Collections.Generic;

namespace Foo.Api.Application.Infrastructure.Services.Nuget
{
    public class NugetPackageDto
    {
        public List<PackageDataDto> Data { get; set; } = new List<PackageDataDto>();
    }

    public class PackageDataDto
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public int TotalDownloads { get; set; }
        public List<VersionsDto> Versions { get; set; } = new List<VersionsDto>();
    }

    public class VersionsDto
    {
        public string Version { get; set; }
        public int Downloads { get; set; }
    }

    public class NugetServiceQueryResponse 
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public int Downloads { get; set; }

        /// <summary>
        /// Confirms if the data response from the nuget api was an exact match
        /// </summary>
        /// <returns></returns>
        public virtual bool Exists()
        {
            return true;
        }
    }

    public class NugetServiceQueryNotFoundResponse : NugetServiceQueryResponse
    {
        ///<inheritdoc/>
        public override bool Exists()
        {
            return false;
        }
    }
}
