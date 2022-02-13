using Dapper;
using Foo.Api.Application.Infrastructure.Common;
using Foo.Api.Domain.Interfaces;
using Foo.Api.Domain.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Foo.Api.Infrastructure.Repositories
{
    public class PackageVersionRepository : IPackageVersionRepository
    {
        private readonly MySqlConnection _context;

        public PackageVersionRepository(IOptions<MySqlOptions> options)
        {
            _context = new MySqlConnection(options.Value.MySqlConnectionString);
        }

        public async Task<PackageVersion> AddAsync(PackageVersion packageVersion)
        {
            using (_context)
            {
                Open();
                var sql = @"
INSERT INTO package_version
(id, id_package, version)
VALUES
(UUID_TO_BIN(@Id), UUID_TO_BIN(@IdPackage), @Version);";

                await _context.ExecuteAsync(sql, packageVersion);

                return packageVersion;
            }
        }

        public async Task<PackageVersion> GetAsync(Guid packageId)
        {
            using (_context)
            {
                Open();
                var sql = @"
SELECT BIN_TO_UUID(id) id, BIN_TO_UUID(id_package) IdPackage, version
FROM package_version
WHERE id_package = UUID_TO_BIN(@PackageId);";

                return await _context.QueryFirstAsync<PackageVersion>(sql, new { packageId });
            }
        }

        public async Task<PackageVersion> UpdateAsync(PackageVersion packageVersion)
        {
            using (_context)
            {
                Open();
                var sql = @"
UPDATE package_version
SET version = @Version
WHERE id = UUID_TO_BIN(@Id);";

                await _context.ExecuteAsync(sql, packageVersion);

                return packageVersion;
            }
        }

        private void Open()
        {
            if (_context.State == ConnectionState.Closed)
                _context.Open();
        }
    }
}
