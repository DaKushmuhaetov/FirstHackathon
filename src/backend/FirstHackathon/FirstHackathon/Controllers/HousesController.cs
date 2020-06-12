using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Models;
using FirstHackathon.Models.Authentication;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IJwtAccessTokenFactory _jwt;
        public HousesController(IHouseRepository houseRepository, FirstHackathonDbContext context, IJwtAccessTokenFactory jwt)
        {
            _houseRepository = houseRepository;
            _context = context;
            _jwt = jwt;
        }

        /// <summary>
        /// Create new house
        /// </summary>
        /// <param name="address">House address</param>
        /// <response code="200">Successfully</response>
        /// <response code="400">Address must be filled</response>
        /// <response code="409">House already exists or login already used</response>
        [HttpPost("/houses/create")]
        [ProducesResponseType(typeof(CreateHouseView), 200)]
        public async Task<ActionResult<CreateHouseView>> Create(
            CancellationToken cancellationToken,
            [FromQuery] string address,
            [FromBody] CreateHouseBinding binding
            )
        {
            if (String.IsNullOrEmpty(address))
                return BadRequest(address);

            if (await _houseRepository.GetByAddress(address, cancellationToken) != null)
                return Conflict(address);

            if (await _context.Houses.SingleOrDefaultAsync(o => o.Login == binding.Login, cancellationToken) != null)
                return Conflict(binding.Login);

            var house = new House(Guid.NewGuid(), address, binding.Login, binding.Password);

            await _houseRepository.Save(house, cancellationToken);

            var token = await _jwt.Create(house, cancellationToken);

            return Ok(new CreateHouseView
            {
                Id = house.Id,
                Address = house.Address,
                LivesHereCounter = house.People.Count(),
                Token = new TokenView { AccessToken = token.Value }
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

        /// <summary>
        /// House authentication by login and password
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="401">Invalid authorization code</response>
        [AllowAnonymous]
        [HttpPost("house/login")]
        [ProducesResponseType(typeof(TokenView), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TokenView>> Authentication(
            CancellationToken cancellationToken,
            [FromBody] AuthenticationBinding binding)
        {
            var house = await _context.Houses
                .SingleOrDefaultAsync(o => o.Login == binding.Login && o.Password == binding.Password, cancellationToken);

            if (house != null)
            {
                var token = await _jwt.Create(house, cancellationToken);
                return new TokenView { AccessToken = token.Value };
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
