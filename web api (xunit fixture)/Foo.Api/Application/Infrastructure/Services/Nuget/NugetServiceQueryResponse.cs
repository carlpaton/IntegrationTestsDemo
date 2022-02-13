namespace Foo.Api.Application.Infrastructure.Services.Nuget
{
    public class NugetServiceQueryResponse 
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public int Downloads { get; set; }

        /// <summary>
        /// Confirms if the data response from the nuget api was an exact match
        /// </summary>
        /// <returns></returns>
        public virtual bool Exists()
        {
            return true;
        }
    }
}
