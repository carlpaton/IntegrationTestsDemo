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
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistsController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistList()
        {
            var artists = await _artistRepository.GetAllAsync();

            // magically no mapping is needed ¯\_(ツ)_/¯

            return Ok(artists);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> AddArtist([FromBody] Artist artist)
        {
            var model = new Domain.Models.Artist(artist.Id, artist.Name);

            await _artistRepository.AddAsync(model);

            return Ok(artist);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteArtist([FromForm] Guid id)
        {
            await _artistRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<Artist>> UpdateArtist([FromForm] Guid id, [FromForm] string name)
        {
            var artist = await _artistRepository.GetAsync(id);

            var model = new Domain.Models.Artist(artist.Id, name);

            await _artistRepository.UpdateAsync(model);

            return Ok(model);
        }
    }
}
