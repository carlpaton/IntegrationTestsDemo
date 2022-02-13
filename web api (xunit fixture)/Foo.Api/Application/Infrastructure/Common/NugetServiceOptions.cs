namespace Foo.Api.Application.Infrastructure.Common
{
    public class NugetServiceOptions
    {
        public const string NugetService = "NugetService";

        public string Url { get; set; }
        public string MaxReceive { get; set; }
    }
}
