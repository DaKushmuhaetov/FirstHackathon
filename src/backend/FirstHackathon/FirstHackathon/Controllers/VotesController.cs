using FirstHackathon.Bindings;
using FirstHackathon.Context;
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
    public class VotesController : ControllerBase
    {
        private readonly FirstHackathonDbContext _context;
        public VotesController(FirstHackathonDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of votings
        /// </summary>
        /// <param name="houseId">House id</param>
        /// <param name="onlyOpened">Returns only active votings</param>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [HttpGet("/houses/{houseId}/votings")]
        [ProducesResponseType(typeof(Page<VotingListItem>), 200)]
        public async Task<ActionResult<Page<VotingListItem>>> GetVotings(
            CancellationToken cancellationToken,
            [FromRoute] Guid houseId,
            [FromQuery] VotingsBinding binding,
            [FromQuery] bool onlyOpened = false
            )
        {
            var query = _context.Votings
                .AsNoTracking()
                .Include(o => o.Variants)
                .Include(o => o.House)
                .Where(o => o.House.Id == houseId)
                .Select(o => new VotingListItem
                {
                    Id = o.Id,
                    Title = o.Title,
                    IsClosed = o.IsClosed,
                    Variants = o.Variants.Select(variant => new VariantView
                    {
                        Id = variant.Id,
                        Title = variant.Title,
                        Votes = variant.Votes.Select(vote => new VoteView
                        {
                            Id = vote.Id,
                            Person = new PersonView
                            {
                                Id = vote.Person.Id,
                                Name = vote.Person.Name,
                                Surname = vote.Person.Surname,
                                House = new HouseView
                                {
                                    Id = vote.Person.House.Id,
                                    Address = vote.Person.House.Address,
                                }
                            }
                        }).ToList()
                    }).ToList()
                });

            if (onlyOpened)
                query = query.Where(o => o.IsClosed == false);

            var items = await query
                .Skip(binding.Offset)
                .Take(binding.Limit)
                .ToListAsync();

            return new Page<VotingListItem>
            {
                Limit = binding.Limit,
                Offset = binding.Offset,
                Total = await query.CountAsync(),
                Items = items
            };
        }
    }
}
