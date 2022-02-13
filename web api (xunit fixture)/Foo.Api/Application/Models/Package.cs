using System;

namespace Foo.Api.Application.Models
{
    public class Package
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public class PackageResponse : Package
    {
        public string Description { get; set; }
        public int TotalDownloads { get; set; }
        public DateTime Created { get; set; }
    }
}
