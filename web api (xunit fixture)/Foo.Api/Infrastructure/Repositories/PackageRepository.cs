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
    public class PackageRepository : IPackageRepository
    {
        private readonly MySqlConnection _context;

        public PackageRepository(IOptions<MySqlOptions> options)
        {
            // this match didnt seem to work so I manually did things like `total_downloads as totalDownloads`
            //DefaultTypeMap.MatchNamesWithUnderscores = true;

            _context = new MySqlConnection(options.Value.MySqlConnectionString);
        }

        public async Task<Package> AddAsync(Package package)
        {
            using (_context)
            {
                Open();
                var sql = @"
INSERT INTO package
(id, name, description, total_downloads)
VALUES
(UUID_TO_BIN(@Id), @Name, @Description, @TotalDownloads);";

                await _context.ExecuteAsync(sql, package);

                return package;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (_context)
            {
                Open();
                var sql = @"
DELETE FROM package
WHERE id = UUID_TO_BIN(@Id);";

                await _context.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            using (_context)
            {
                Open();
                var sql = @"
SELECT BIN_TO_UUID(id) id, name, description, total_downloads as totalDownloads, created
FROM package
ORDER BY name;";

                return await _context.QueryAsync<Package>(sql);
            }
        }

        public async Task<Package> GetAsync(Guid id)
        {
            using (_context)
            {
                Open();
                var sql = @"
SELECT BIN_TO_UUID(id) id, name, description, total_downloads as totalDownloads, created
FROM package
WHERE id = UUID_TO_BIN(@Id);";

                return await _context.QueryFirstAsync<Package>(sql, new { Id = id });
            }
        }

        public async Task<Package> UpdateAsync(Package package)
        {
            using (_context)
            {
                Open();
                var sql = @"
UPDATE package
SET name = @Name, description = @Description, total_downloads = @TotalDownloads
WHERE id = UUID_TO_BIN(@Id);";

                await _context.ExecuteAsync(sql, package);

                return package;
            }
        }

        private void Open()
        {
            if (_context.State == ConnectionState.Closed)
                _context.Open();
        }
    }
}
