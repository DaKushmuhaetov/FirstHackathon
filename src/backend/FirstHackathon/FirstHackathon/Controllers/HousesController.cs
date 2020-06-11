using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Models;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Controllers
{
    [ApiController]
    public class HousesController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        private readonly FirstHackathonDbContext _context;
        public HousesController(IHouseRepository houseRepository, FirstHackathonDbContext context)
        {
            _houseRepository = houseRepository;
            _context = context;
        }

        /// <summary>
        /// Create new house
        /// </summary>
        /// <param name="address">House address</param>
        /// <response code="200">Successfully</response>
        [HttpPost("/houses/create")]
        [ProducesResponseType(typeof(House), 200)]
        public async Task<ActionResult<House>> Create(
            CancellationToken cancellationToken,
            [FromQuery] string address,
            [FromBody] CreateHouseBinding binding
            )
        {
            var house = new House(Guid.NewGuid(), address, binding.Login, binding.Password);

            await _houseRepository.Save(house);

            return Ok(house);
        }

        /// <summary>
        /// Get list of houses
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [HttpGet("/houses/")]
        [ProducesResponseType(typeof(Page<HouseListItem>), 200)]
        public async Task<Page<HouseListItem>> GetHouses(
            CancellationToken cancellationToken,
            [FromQuery]GetHouseListBinding binding)
        {
            var query = _context.Houses
                .AsNoTracking()
                .Select(o => new HouseListItem { 
                    Id = o.Id,
                    Address = o.Address
                });

            var items = await query
                .Skip(binding.Offset)
                .Take(binding.Limit)
                .ToListAsync();

            return new Page<HouseListItem>
            {
                Limit = binding.Limit,
                Offset = binding.Offset,
                Total = await query.CountAsync(),
                Items = items
            };
        }
    }
}
