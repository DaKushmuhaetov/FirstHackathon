using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Models;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Mvc;
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
        /// <response code="400">Address must be filled</response>
        /// <response code="409">House already exists</response>
        [HttpPost("/houses/create")]
        [ProducesResponseType(typeof(HouseView), 200)]
        public async Task<ActionResult<HouseView>> Create(
            CancellationToken cancellationToken,
            [FromQuery] string address,
            [FromBody] CreateHouseBinding binding
            )
        {
            if (String.IsNullOrEmpty(address))
                return BadRequest(address);

            if (await _houseRepository.GetByAddress(address, cancellationToken) != null)
                return Conflict(address);

            var house = new House(Guid.NewGuid(), address, binding.Login, binding.Password);

            await _houseRepository.Save(house, cancellationToken);

            return Ok(new HouseView
            {
                Id = house.Id,
                Address = house.Address,
                LivesHereCounter = house.People.Count()
            });
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
            [FromQuery] GetHouseListBinding binding)
        {
            var query = _context.Houses
                .AsNoTracking()
                .Select(o => new HouseListItem
                {
                    Id = o.Id,
                    Address = o.Address,
                    LivesHereCounter = o.People.Count()
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
