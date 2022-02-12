using Foo.Api.Application.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Foo.Api.Application.Models
{
    public class Artist
    {
        public Artist(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [ModelBinder(BinderType = typeof(JsonModelBinder))]
        public Guid Id { get; private set; }

        [ModelBinder(BinderType = typeof(JsonModelBinder))]
        public string Name { get; private set; }
    }
}
