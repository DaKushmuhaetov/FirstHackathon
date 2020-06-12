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
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly FirstHackathonDbContext _context;
        private readonly IJwtAccessTokenFactory _jwt;
        public PeopleController(IPersonRepository personRepository,
            IHouseRepository houseRepository,
            FirstHackathonDbContext context,
            IJwtAccessTokenFactory jwt)
        {
            _personRepository = personRepository;
            _houseRepository = houseRepository;
            _context = context;
            _jwt = jwt;
        }

        /// <summary>
        /// Create new person in house
        /// </summary>
        /// <param name="houseId">House id</param>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="404">House not found</response>
        /// <response code="409">Person with this login already exists</response>
        [HttpPost("/houses/{houseId}/add")]
        [ProducesResponseType(typeof(CreatePersonView), 200)]
        public async Task<ActionResult<CreatePersonView>> Create(
            CancellationToken cancellationToken,
            [FromRoute] Guid houseId,
            [FromBody] CreatePersonBinding binding)
        {
            if (await _personRepository.GetByLogin(binding.Login, cancellationToken) != null)
                return Conflict();

            var house = await _houseRepository.Get(houseId, cancellationToken);
            if (house == null)
                return NotFound(houseId);

            var person = new Person(Guid.NewGuid(), binding.Name, binding.Surname, binding.Login, binding.Password, house);

            await _personRepository.Save(person, cancellationToken);

            var token = await _jwt.Create(person, cancellationToken);

            return Ok(new CreatePersonView
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                House = new HouseView
                {
                    Id = house.Id,
                    Address = house.Address,
                    LivesHereCounter = house.People.Count()
                },
                Token = new TokenView { AccessToken = token.Value }
            });
        }

        /// <summary>
        /// Person authentication by login and password
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="401">Invalid authorization code</response>
        [AllowAnonymous]
        [HttpPost("person/login")]
        [ProducesResponseType(typeof(TokenView), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TokenView>> Authentication(
            CancellationToken cancellationToken,
            [FromBody] AuthenticationBinding binding)
        {
            var person = await _context.People
                .Include(o => o.House)
                .SingleOrDefaultAsync(o => o.Login == binding.Login && o.Password == binding.Password, cancellationToken);

            if (person != null)
            {
                var token = await _jwt.Create(person, cancellationToken);
                return new TokenView { AccessToken = token.Value };
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Token validation for person
        /// </summary>
        /// <response code="200">Successfully</response>
        /// <response code="401">Invalid authorization code</response>
        [Authorize(AuthenticationSchemes = "person")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("/person/token")]
        public async Task<ActionResult> IsTokenValid(
            CancellationToken cancellationToken)
        {
            var personId = User.GetId();
            return await _personRepository.Get(personId, cancellationToken) == null ? Unauthorized() : (ActionResult)NoContent();
        }
    }
}
