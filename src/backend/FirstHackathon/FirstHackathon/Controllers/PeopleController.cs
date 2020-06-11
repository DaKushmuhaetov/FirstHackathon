using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Models;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Mvc;
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
        public PeopleController(IPersonRepository personRepository,
            IHouseRepository houseRepository,
            FirstHackathonDbContext context)
        {
            _personRepository = personRepository;
            _houseRepository = houseRepository;
            _context = context;
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
        [ProducesResponseType(typeof(PersonView), 200)]
        public async Task<ActionResult<PersonView>> Create(
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

            return Ok(new PersonView
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                House = new HouseView
                {
                    Id = house.Id,
                    Address = house.Address,
                    LivesHereCounter = house.People.Count()
                }
            });
        }
    }
}
