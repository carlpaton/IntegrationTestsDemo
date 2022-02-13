using System;

namespace Foo.Api.Domain.Models
{
    public class Package
    {
        /// <summary>
        /// Nuget package model
        /// </summary>
        /// <param name="id">
        /// Mysql BIN_TO_UUID(id) gives this to dapper as a string.
        /// </param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="totaldownloads"></param>
        /// <param name="created"></param>
        public Package(string id, string name, string description,
            int totaldownloads, DateTime created)
        {
            Id = Guid.Parse(id);
            Name = name;
            Description = description;
            TotalDownloads = totaldownloads;
            Created = created;
        }

        public Package(Guid id, string name, string description,
            int totaldownloads, DateTime created)
        {
            Id = id;
            Name = name;
            Description = description;
            TotalDownloads = totaldownloads;
            Created = created;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TotalDownloads { get; private set; }
        public DateTime Created { get; private set; }
    }
}
