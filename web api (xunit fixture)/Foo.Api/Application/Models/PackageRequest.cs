using System;

namespace Foo.Api.Application.Models
{
    public class PackageRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
