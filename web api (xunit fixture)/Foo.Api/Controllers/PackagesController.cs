using Foo.Api.Application.Models;
using Foo.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPackageVersionRepository _packageVersionRepository;

        public PackagesController(IPackageRepository packageRepository, IPackageVersionRepository packageVersionRepository)
        {
            _packageRepository = packageRepository;
            _packageVersionRepository = packageVersionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageResponse>>> GetList()
        {
            var packages = await _packageRepository.GetAllAsync();

            var packageResponse = new List<PackageResponse>();

            foreach (var package in packages) 
            {
                var packageVersion = await _packageVersionRepository.GetAsync(package.Id);

                packageResponse.Add(new PackageResponse() { 
                    Created = package.Created,
                    Description = package.Description,
                    Id = package.Id,
                    Name = package.Name,
                    TotalDownloads = package.TotalDownloads,
                    Version = packageVersion.Version
                });
            }

            return Ok(packageResponse);
        }

        [HttpPost]
        public async Task<ActionResult<PackageResponse>> Add([FromBody] Package package)
        {
            var newPackage = new Domain.Models.Package(
                package.Id, 
                package.Name,
                "description",
                0,
                DateTime.Now);

            await _packageRepository.AddAsync(newPackage);

            var newPackageVersion = new Domain.Models.PackageVersion(
                Guid.NewGuid(),
                newPackage.Id,
                package.Version);

            await _packageVersionRepository.AddAsync(newPackageVersion);

            var packageResponse = new PackageResponse() 
            { 
                Created = newPackage.Created,
                Description = newPackage.Description,
                Id = newPackage.Id,
                Name = newPackage.Name,
                TotalDownloads = newPackage.TotalDownloads,
                Version = newPackageVersion.Version
            };

            return Ok(packageResponse);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromForm] Guid id)
        {
            await _packageRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<PackageResponse>> Update([FromBody] Package package)
        {
            var existingPackage = await _packageRepository.GetAsync(package.Id);
            var existingPackageVersion = await _packageVersionRepository.GetAsync(existingPackage.Id);

            var updatedPackage = new Domain.Models.Package(
                existingPackage.Id, 
                package.Name,
                "updated description",
                0,
                DateTime.Now);

            var updatedPackageVersion = new Domain.Models.PackageVersion(
                existingPackageVersion.Id,
                existingPackageVersion.IdPackage,
                package.Version);

            await _packageRepository.UpdateAsync(updatedPackage);
            await _packageVersionRepository.UpdateAsync(updatedPackageVersion);

            var packageResponse = new PackageResponse()
            {
                Created = updatedPackage.Created,
                Description = updatedPackage.Description,
                Id = updatedPackage.Id,
                Name = updatedPackage.Name,
                TotalDownloads = updatedPackage.TotalDownloads,
                Version = updatedPackageVersion.Version
            };

            return Ok(packageResponse);
        }
    }
}
