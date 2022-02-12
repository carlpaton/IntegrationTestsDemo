using System;

namespace Foo.Api.Domain.Models
{
    public class Artist
    {
        /// <summary>
        /// Mysql BIN_TO_UUID(id) gives this to dapper as a string. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Artist(string id, string name)
        {
            Id = Guid.Parse(id);
            Name = name;
        }

        public Artist(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
