namespace Foo.Api.Application.Infrastructure.Services.Nuget
{
    public class NugetServiceQueryNotFoundResponse : NugetServiceQueryResponse
    {
        ///<inheritdoc/>
        public override bool Exists()
        {
            return false;
        }
    }
}
