using Dapper;
using Foo.Api.Application.Infrastructure.Common;
using Foo.Api.Domain.Interfaces;
using Foo.Api.Domain.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Foo.Api.Infrastructure.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MySqlConnection _context;

        public ArtistRepository(IOptions<MySqlOptions> options)
        {
            _context = new MySqlConnection(options.Value.MySqlConnectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            using (_context)
            {
                Open();
                var sql = @"
SELECT BIN_TO_UUID(id) id, name 
FROM artist
ORDER BY name;";

                return await _context.QueryAsync<Artist>(sql);
            }
        }

        public async Task<Artist> GetAsync(Guid id)
        {
            using (_context)
            {
                Open();
                var sql = @"
SELECT BIN_TO_UUID(id) id, name 
FROM artist
WHERE id = UUID_TO_BIN(@Id);";

                return await _context.QueryFirstAsync<Artist>(sql, new { Id = id });
            }
        }

        public async Task<Artist> AddAsync(Artist artist)
        {
            using (_context)
            {
                Open();
                var sql = @"
INSERT INTO artist
(id, name)
VALUES
(UUID_TO_BIN(@Id), @Name);";

                await _context.ExecuteAsync(sql, artist);

                return artist;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (_context)
            {
                Open();
                var sql = @"
DELETE FROM artist
WHERE id = UUID_TO_BIN(@Id);";

                await _context.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<Artist> UpdateAsync(Artist artist)
        {
            using (_context)
            {
                Open();
                var sql = @"
UPDATE artist
SET name = @Name
WHERE id = UUID_TO_BIN(@Id);";

                await _context.ExecuteAsync(sql, artist);

                return artist;
            }
        }

        private void Open()
        {
            if (_context.State == ConnectionState.Closed)
                _context.Open();
        }
    }
}
