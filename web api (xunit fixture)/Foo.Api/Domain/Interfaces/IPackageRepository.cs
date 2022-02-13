using Foo.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foo.Api.Domain.Interfaces
{
    public interface IPackageRepository
    {
        /// <summary>
        /// Get package by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Package> GetAsync(Guid id);

        /// <summary>
        /// Get all packages
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Package>> GetAllAsync();

        /// <summary>
        /// Adds the new packages
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        Task<Package> AddAsync(Package package);

        /// <summary>
        /// Deletes the package
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Updates the package
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        Task<Package> UpdateAsync(Package package);
    }
}
