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
        private readonly IVulnerabilityRepository _vulnerabilityRepository;

        public PackagesController(IPackageRepository packageRepository, IPackageVersionRepository packageVersionRepository,
            IVulnerabilityRepository vulnerabilityRepository)
        {
            _packageRepository = packageRepository;
            _packageVersionRepository = packageVersionRepository;
            _vulnerabilityRepository = vulnerabilityRepository;
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
                    Vulnerability = new List<PackageVulnerability>() 
                    { 
                        new PackageVulnerability()
                        {                        
                            Version = packageVersion.Version
                        }
                    }
                });
            }

            return Ok(packageResponse);
        }

        [HttpPost]
        public async Task<ActionResult<PackageResponse>> Add([FromBody] PackageRequest packageRequest)
        {
            var newPackage = new Domain.Models.Package(
                packageRequest.Id, 
                packageRequest.Name,
                "description",
                0,
                DateTime.Now);

            var newPackageVersion = new Domain.Models.PackageVersion(
                Guid.NewGuid(),
                newPackage.Id,
                packageRequest.Version);

            var newVulnerability = new Domain.Models.Vulnerability(
                Guid.NewGuid(),
                newPackageVersion.Id,
                "title",
                "description",
                "cvss score",
                "reference");

            await _packageRepository.AddAsync(newPackage);

            await _packageVersionRepository.AddAsync(newPackageVersion);

            await _vulnerabilityRepository.AddAsync(newVulnerability);

            var packageResponse = new PackageResponse() 
            { 
                Created = newPackage.Created,
                Description = newPackage.Description,
                Id = newPackage.Id,
                Name = newPackage.Name,
                TotalDownloads = newPackage.TotalDownloads,
                Vulnerability = new List<PackageVulnerability>()
                {
                    new PackageVulnerability()
                    {
                        Version = newPackageVersion.Version
                    }
                }
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
        public async Task<ActionResult<PackageResponse>> Update([FromBody] PackageRequest packageRequest)
        {
            var existingPackage = await _packageRepository.GetAsync(packageRequest.Id);
            var existingPackageVersion = await _packageVersionRepository.GetAsync(existingPackage.Id);

            var updatedPackage = new Domain.Models.Package(
                existingPackage.Id, 
                packageRequest.Name,
                "updated description",
                0,
                DateTime.Now);

            var updatedPackageVersion = new Domain.Models.PackageVersion(
                existingPackageVersion.Id,
                existingPackageVersion.IdPackage,
                packageRequest.Version);

            await _packageRepository.UpdateAsync(updatedPackage);
            await _packageVersionRepository.UpdateAsync(updatedPackageVersion);

            var packageResponse = new PackageResponse()
            {
                Created = updatedPackage.Created,
                Description = updatedPackage.Description,
                Id = updatedPackage.Id,
                Name = updatedPackage.Name,
                TotalDownloads = updatedPackage.TotalDownloads,
                Vulnerability = new List<PackageVulnerability>()
                {
                    new PackageVulnerability()
                    {
                        Version = updatedPackageVersion.Version
                    }
                }
            };

            return Ok(packageResponse);
        }
    }
}
