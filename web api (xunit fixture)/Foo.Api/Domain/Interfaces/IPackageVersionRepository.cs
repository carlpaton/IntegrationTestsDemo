using Foo.Api.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Foo.Api.Domain.Interfaces
{
    public interface IPackageVersionRepository
    {
        /// <summary>
        /// Get package version by package id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PackageVersion> GetAsync(Guid packageId);

        /// <summary>
        /// Insert the package version
        /// </summary>
        /// <param name="packageVersion"></param>
        /// <returns></returns>
        Task<PackageVersion> AddAsync(PackageVersion packageVersion);

        /// <summary>
        /// Update the package version
        /// </summary>
        /// <param name="packageVersion"></param>
        /// <returns></returns>
        Task<PackageVersion> UpdateAsync(PackageVersion packageVersion);
    }
}
