using System.Text.Json;

namespace Foo.Api.Application.Infrastructure.Common
{
    public static class SerializationOptions
    {
        /// <summary>
        /// Deserialize objects from remote network access services
        /// </summary>
        public static JsonSerializerOptions Deserialize { get; } = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
