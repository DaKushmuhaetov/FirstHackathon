using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Extensions;
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
        private readonly ICreatePersonRepository _createPersonRepository;
        private readonly FirstHackathonDbContext _context;
        private readonly IJwtAccessTokenFactory _jwt;
        public HousesController(
            IHouseRepository houseRepository,
            ICreatePersonRepository createPersonRepository,
            FirstHackathonDbContext context,
            IJwtAccessTokenFactory jwt)
        {
            _houseRepository = houseRepository;
            _createPersonRepository = createPersonRepository;
            _context = context;
            _jwt = jwt;
        }

        /// <summary>
        /// Create new house [anonymous]
        /// </summary>
        /// <param name="address">House address</param>
        /// <response code="200">Successfully</response>
        /// <response code="400">Address must be filled</response>
        /// <response code="409">House already exists or login already used</response>
        [AllowAnonymous]
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
        /// Get list of houses [anonymous]
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [AllowAnonymous]
        [HttpGet("/houses/")]
        [ProducesResponseType(typeof(Page<HouseListItem>), 200)]
        public async Task<Page<HouseListItem>> GetHouses(
            CancellationToken cancellationToken,
            [FromQuery] DefaultListBinding binding)
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

        #region Person

        /// <summary>
        /// Get list of people lives here [admin,person]
        /// </summary>
        /// <response code="200">Successfully</response>
        [Authorize(AuthenticationSchemes = "admin,person")]
        [HttpGet("/house/people")]
        [ProducesResponseType(typeof(Page<PersonView>), 200)]
        public async Task<Page<PersonView>> GetPeople(
            CancellationToken cancellationToken,
            [FromQuery] DefaultListBinding binding)
        {
            var address = User.GetAddress();
            var house = await _houseRepository.GetByAddress(address, cancellationToken);

            var query = _context.People
                .AsNoTracking()
                .Include(o => o.House)
                .Where(o => o.House == house)
                .Select(o => new PersonView
                {
                    Id = o.Id,
                    Name = o.Name,
                    Surname = o.Surname
                });

            var items = await query
                .Skip(binding.Offset)
                .Take(binding.Limit)
                .ToListAsync();

            return new Page<PersonView>
            {
                Limit = binding.Limit,
                Offset = binding.Offset,
                Total = await query.CountAsync(),
                Items = items
            };
        }

        /// <summary>
        /// Get list of person registration requests [admin]
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpGet("/house/requests")]
        [ProducesResponseType(typeof(Page<CreatePersonRequestListItem>), 200)]
        public async Task<Page<CreatePersonRequestListItem>> GetCreatePersonRequests(
            CancellationToken cancellationToken,
            [FromQuery] DefaultListBinding binding)
        {
            var houseId = User.GetId();

            var query = _context.CreatePersonRequests
                .AsNoTracking()
                .Include(o => o.House)
                .Where(o => o.House.Id == houseId)
                .Select(o => new CreatePersonRequestListItem
                {
                    Id = o.Id,
                    Name = o.Name,
                    Surname = o.Surname,
                    Login = o.Login,
                    Password = o.Password
                });

            var items = await query
                .Skip(binding.Offset)
                .Take(binding.Limit)
                .ToListAsync();

            return new Page<CreatePersonRequestListItem>
            {
                Limit = binding.Limit,
                Offset = binding.Offset,
                Total = await query.CountAsync(),
                Items = items
            };
        }

        /// <summary>
        /// Reject person registration request [admin]
        /// </summary>
        /// <param name="requestId">Person registration request id</param>
        /// <response code="200">Successfully</response>
        /// <response code="404">Request not found</response>
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpPost("/house/person/{requestId}/reject")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CreatePersonView>> RejectPersonRequest(
            CancellationToken cancellationToken,
            [FromRoute] Guid requestId)
        {
            var request = await _createPersonRepository.Get(requestId, cancellationToken);
            if (request == null)
                return NotFound($"Request not found: {requestId}");

            if (request.House.Id != User.GetId())
                return Unauthorized();

            await _createPersonRepository.Reject(request.Id, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Accept person registration request [admin]
        /// </summary>
        /// <param name="requestId">Person registration request id</param>
        /// <response code="200">Successfully</response>
        /// <response code="404">Request not found</response>
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpPost("/house/person/{requestId}/accept")]
        [ProducesResponseType(typeof(CreatePersonView), 200)]
        public async Task<ActionResult<CreatePersonView>> AcceptPersonRequest(
            CancellationToken cancellationToken,
            [FromRoute] Guid requestId)
        {
            var request = await _createPersonRepository.Get(requestId, cancellationToken);
            if (request == null)
                return NotFound($"Request not found: {requestId}");

            if (request.House.Id != User.GetId())
                return Unauthorized();

            await _createPersonRepository.Accept(request.Id, cancellationToken);

            return Ok(new CreatePersonView
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Login = request.Login,
                Password = request.Password
            });
        }

        /// <summary>
        /// Create new request for person registration [anonymous]
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="404">House not found</response>
        /// <response code="409">Person with this login already exists</response>
        [AllowAnonymous]
        [HttpPost("/house/{houseId}/person")]
        [ProducesResponseType(typeof(CreatePersonView), 200)]
        public async Task<ActionResult<CreatePersonView>> CreatePersonRequest(
            CancellationToken cancellationToken,
            [FromRoute] Guid houseId,
            [FromBody] CreatePersonBinding binding)
        {
            try
            {
                var house = await _houseRepository.Get(houseId, cancellationToken);
                if (house == null)
                    return NotFound($"House not found: {houseId}");

                var person = new CreatePersonRequest(Guid.NewGuid(), binding.Name, binding.Surname, binding.Login, binding.Password, house);

                await _createPersonRepository.Create(person, cancellationToken);

                return Ok(new CreatePersonView
                {
                    Id = person.Id,
                    Name = person.Name,
                    Surname = person.Surname,
                    Login = person.Login,
                    Password = person.Password
                });
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(exception.Message);
            }
        }

        #endregion
    }
}
