using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Extensions;
using FirstHackathon.Models;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
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
            _newsRepository = newsRepository;
            _context = context;
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
                    CreateDate = o.CreateDate,
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

        /// <summary>
        /// Create new news post [admin]
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [HttpPost("/house/news")]
        [Consumes("application/octet-stream")]
        [ProducesResponseType(typeof(NewsPostView), 200)]
        [Authorize(AuthenticationSchemes = "admin")]
        public async Task<ActionResult<NewsPostView>> Create(
            CancellationToken cancellationToken,
            [FromQuery] CreateNewsPostBinding binding)
        {
            var house = await _houseRepository.GetByAddress(User.GetAddress(), cancellationToken);

            await using var ms = new MemoryStream();
            await Request.Body.CopyToAsync(ms, cancellationToken);

            var post = new NewsPost(Guid.NewGuid(), binding.Title, binding.Description, ms.ToArray(), DateTime.UtcNow, house);

            await _newsRepository.Save(post, cancellationToken);

            return Ok(new NewsPostView
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                CreateDate = post.CreateDate,
                Image = post.Image
            });
        }

        /// <summary>
        /// Remove news post [admin]
        /// </summary>
        /// <param name="newsPostId">News post id</param>
        /// <response code="204">Successfully</response>
        /// <response code="404">News post not found</response>
        [HttpDelete("/house/news/{newsPostId}")]
        [Authorize(AuthenticationSchemes = "admin")]
        public async Task<ActionResult> Remove(
            CancellationToken cancellationToken,
            [FromRoute]Guid newsPostId)
        {
            var newsPost = await _newsRepository.Get(newsPostId, cancellationToken);

            if (newsPost == null)
                return NotFound($"News post not found: {newsPostId}");

            var house = await _houseRepository.GetByAddress(User.GetAddress(), cancellationToken);

            if (house == null)
                return Unauthorized();
            if (newsPost.House != house)
                return Unauthorized();

            _context.News.Remove(newsPost);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
