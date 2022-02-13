# Model Binder

Converts incoming request data into strongly typed key arguments.

## Model

```c#
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
```

## JsonModelBinder

```c#
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foo.Api.Application.Infrastructure.ModelBinders
{
    public class JsonModelBinder : IModelBinder
    {
        private readonly JsonSerializerOptions serializerOption = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider
                .GetValue(bindingContext.FieldName);

            if (string.IsNullOrEmpty(value.FirstValue))
                return Task.CompletedTask;

            var result = JsonSerializer
                .Deserialize(value.FirstValue, bindingContext.ModelType, serializerOption);
            
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }
}

```

- https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding#custom-model-binder-sample