using Foo.Api.Application.Infrastructure.Services.Nuget;
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
        private readonly INugetServiceClient _nugetServiceClient;

        public PackagesController(IPackageRepository packageRepository, IPackageVersionRepository packageVersionRepository,
            IVulnerabilityRepository vulnerabilityRepository, INugetServiceClient nugetServiceClient)
        {
            _packageRepository = packageRepository;
            _packageVersionRepository = packageVersionRepository;
            _vulnerabilityRepository = vulnerabilityRepository;
            _nugetServiceClient = nugetServiceClient;
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
            var nugetPackage = await _nugetServiceClient.QueryPackageAsync(
                packageRequest.Name,
                packageRequest.Version);

            if (!nugetPackage.Exists()) 
            {
                return Problem($"Package {packageRequest.Name}@{packageRequest.Version} was not found on Nuget API.");
            }

            var newPackage = new Domain.Models.Package(
                packageRequest.Id,
                nugetPackage.Name,
                nugetPackage.Description,
                nugetPackage.Downloads,
                DateTime.Now);

            var newPackageVersion = new Domain.Models.PackageVersion(
                Guid.NewGuid(),
                newPackage.Id,
                packageRequest.Version);

            // TODO call out to OSS api for vulnerability details

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
