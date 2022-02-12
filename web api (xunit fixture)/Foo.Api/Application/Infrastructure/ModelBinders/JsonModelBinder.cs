using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foo.Api.Application.Infrastructure.ModelBinders
{
    public class JsonModelBinder : IModelBinder
    {
        private readonly JsonSerializerOptions serializerOption = new JsonSerializerOptions
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
