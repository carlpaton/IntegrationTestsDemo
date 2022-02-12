using Foo.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foo.Api.Domain.Interfaces
{
    public interface IArtistRepository
    {
        /// <summary>
        /// Get all artists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Artist>> GetAllAsync();

        /// <summary>
        /// Get artist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Artist> GetAsync(Guid id);

        /// <summary>
        /// Adds the new artist
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        Task<Artist> AddAsync(Artist artist);

        /// <summary>
        /// Deletes artist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Updates the given artist
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        Task<Artist> UpdateAsync(Artist artist);
    }
}
