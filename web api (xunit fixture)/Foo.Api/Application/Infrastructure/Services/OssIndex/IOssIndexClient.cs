using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foo.Api.Application.Infrastructure.Services.OssIndex
{
    public interface IOssIndexClient
    {
        /// <summary>
        /// Gets a repport of OSS Index vulnerabilities for the given component
        /// </summary>
        /// <param name="coordinates">
        /// Example shape `pkg:nuget/log4net@1.2.10`
        /// </param>
        /// <returns>
        /// A collection of vulnerabilities.
        /// An empty collection response means nothing was found
        /// </returns>
        public Task<IEnumerable<OssIndexResponse>> GetComponentReport(string coordinates);
    }
}
