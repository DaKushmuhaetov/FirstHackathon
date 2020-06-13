using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Extensions;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Controllers
{
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IPersonRepository _personRepository;
        private readonly FirstHackathonDbContext _context;
        private readonly INewsRepository _newsRepository;
        public NewsController(
            IHouseRepository houseRepository,
            IPersonRepository personRepository,
            FirstHackathonDbContext context,
            INewsRepository newsRepository
            )
        {
            _houseRepository = houseRepository;
            _personRepository = personRepository;
            _context = context;
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Get list of news [admin,person]
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        /// <response code="404">House not found</response>
        [Authorize(AuthenticationSchemes = "admin,person")]
        [HttpGet("/house/news")]
        [ProducesResponseType(typeof(Page<NewsPostView>), 200)]
        public async Task<ActionResult<Page<NewsPostView>>> GetVotingsPerson(
            CancellationToken cancellationToken,
            [FromQuery] DateListBinding binding
            )
        {
            var house = await _houseRepository.GetByAddress(User.GetAddress(), cancellationToken);
            if (house == null)
                return NotFound($"House not found");

            var query = _context.News
                .AsNoTracking()
                .Include(o => o.House)
                .Where(o => o.House.Id == house.Id)
                .Where(o => binding.StartDate == null ? true : o.CreateDate >= binding.StartDate)
                .Where(o => binding.EndDate == null ? true : o.CreateDate <= binding.EndDate)
                .Select(o => new NewsPostView
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    Image = o.Image
                });

            var items = await query
                .Skip(binding.Offset)
                .Take(binding.Limit)
                .ToListAsync();

            return new Page<NewsPostView>
            {
                Limit = binding.Limit,
                Offset = binding.Offset,
                Total = await query.CountAsync(),
                Items = items
            };
        }
    }
}
