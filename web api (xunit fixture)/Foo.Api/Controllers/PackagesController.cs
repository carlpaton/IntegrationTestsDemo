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

        public PackagesController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageResponse>>> GetList()
        {
            var models = await _packageRepository.GetAllAsync();

            var packages = new List<PackageResponse>();
            foreach (var model in models) 
            {
                packages.Add(new PackageResponse() { 
                    Created = model.Created,
                    Description = model.Description,
                    Id = model.Id,
                    Name = model.Name,
                    TotalDownloads = model.TotalDownloads,
                    Version = ""
                });
            }

            return Ok(packages);
        }

        [HttpPost]
        public async Task<ActionResult<PackageResponse>> Add([FromBody] Package package)
        {
            var model = new Domain.Models.Package(
                package.Id, 
                package.Name,
                "description",
                0,
                DateTime.Now);

            await _packageRepository.AddAsync(model);

            var packageResponse = new PackageResponse() 
            { 
                Created = model.Created,
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                TotalDownloads = model.TotalDownloads,
                Version = ""  
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
        public async Task<ActionResult<Artist>> UpdateArtist([FromBody] Package updatePackage)
        {
            var existingModel = await _packageRepository.GetAsync(updatePackage.Id);

            var model = new Domain.Models.Package(
                existingModel.Id, 
                updatePackage.Name,
                "updated description",
                0,
                DateTime.Now);

            // TODO update version in `foo_api.package_version`

            await _packageRepository.UpdateAsync(model);

            return Ok();
        }
    }
}
