using System;

namespace Foo.Api.Domain.Models
{
    public class PackageVersion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">
        /// Mysql BIN_TO_UUID(id) gives this to dapper as a string.
        /// </param>
        /// <param name="idPackage">
        ///  Mysql BIN_TO_UUID(id) gives this to dapper as a string.
        /// </param>
        /// <param name="version"></param>
        public PackageVersion(string id, string idPackage, string version)
        {
            Id = Guid.Parse(id);
            IdPackage = Guid.Parse(idPackage);
            Version = version;
        }

        public PackageVersion(Guid id, Guid idPackage, string version)
        {
            Id = id;
            IdPackage = idPackage;
            Version = version;
        }

        public Guid Id { get; private set; }
        /// <summary>
        /// Links to Package.Id
        /// </summary>
        public Guid IdPackage { get; private set; }
        public string Version { get; private set; }
    }
}
