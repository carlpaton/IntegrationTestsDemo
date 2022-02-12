using System;

namespace Foo.Api.Domain.Models
{
    public class Song
    {
        public int Id { get; private set; }
        public int IdArtist { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
    }
}
