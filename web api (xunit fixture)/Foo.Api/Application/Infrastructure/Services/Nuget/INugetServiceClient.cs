using System.Threading.Tasks;

namespace Foo.Api.Application.Infrastructure.Services.Nuget
{
    public interface INugetServiceClient
    {
        /// <summary>
        /// Searches for the given nuget package.
        /// </summary>
        /// <param name="packageName">
        /// Needs to be an exact match on nuget.
        /// </param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Task<NugetServiceQueryResponse> QueryPackageAsync(string packageName, string version);
    }
}
