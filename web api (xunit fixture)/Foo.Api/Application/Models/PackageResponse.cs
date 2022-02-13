using System;
using System.Collections.Generic;

namespace Foo.Api.Application.Models
{
    public class PackageResponse : Package
    {
        public string Description { get; set; }
        public int TotalDownloads { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<PackageVulnerability> Vulnerability { get; set; }
    }
}
