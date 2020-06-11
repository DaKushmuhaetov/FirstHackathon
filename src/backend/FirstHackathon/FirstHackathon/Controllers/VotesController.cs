using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Models.Votes;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        [HttpGet("/houses/{houseId}/votings")]
        public async Task<ActionResult<Page<VotingListItem>>> GetVotings(
            CancellationToken cancellationToken,
            [FromQuery]VotingsBinding binding
            )
        {
            var query = _context.Votings
                .AsNoTracking()
                .Include(o => o.Variants)
                .Select(o => new VotingListItem
                {
                    Id = o.Id,
                    Title = o.Title,
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
