using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Extensions;
using FirstHackathon.Models.Authentication;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly ICreatePersonRepository _createPersonRepository;
        private readonly FirstHackathonDbContext _context;
        private readonly IJwtAccessTokenFactory _jwt;
        public AuthController(IPersonRepository personRepository,
            IHouseRepository houseRepository,
            ICreatePersonRepository createPersonRepository,
            FirstHackathonDbContext context,
            IJwtAccessTokenFactory jwt)
        {
            _personRepository = personRepository;
            _houseRepository = houseRepository;
            _createPersonRepository = createPersonRepository;
            _context = context;
            _jwt = jwt;
        }

        #region TokenValidation

        /// <summary>
        /// Token validation for person
        /// </summary>
        /// <response code="200">Successfully</response>
        /// <response code="401">Invalid authorization code</response>
        [Authorize(AuthenticationSchemes = "person")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("/person/token")]
        public async Task<ActionResult> IsPersonTokenValid(
            CancellationToken cancellationToken)
        {
            var personId = User.GetId();
            return await _personRepository.Get(personId, cancellationToken) == null ? Unauthorized() : (ActionResult)NoContent();
        }

        /// <summary>
        /// Token validation for house
        /// </summary>
        /// <response code="200">Successfully</response>
        /// <response code="401">Invalid authorization code</response>
        [Authorize(AuthenticationSchemes = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpPost("/house/token")]
        public async Task<ActionResult> IsHouseTokenValid(
            CancellationToken cancellationToken)
        {
            var houseId = User.GetId();
            return await _houseRepository.Get(houseId, cancellationToken) == null ? Unauthorized() : (ActionResult)NoContent();
        }

        #endregion

        #region Authentication

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
        public async Task<ActionResult<TokenView>> PersonAuthentication(
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
        /// House authentication by login and password
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="401">Invalid authorization code</response>
        [AllowAnonymous]
        [HttpPost("house/login")]
        [ProducesResponseType(typeof(TokenView), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TokenView>> HouseAuthentication(
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

        #endregion
    }
}
