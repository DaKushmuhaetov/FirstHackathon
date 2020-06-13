using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Extensions;
using FirstHackathon.Models.Votes;
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
    public class VotesController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IPersonRepository _personRepository;
        private readonly FirstHackathonDbContext _context;
        private readonly IVotingRepository _votingRepository;
        public VotesController(
            IHouseRepository houseRepository,
            IPersonRepository personRepository,
            FirstHackathonDbContext context,
            IVotingRepository votingRepository
            )
        {
            _houseRepository = houseRepository;
            _personRepository = personRepository;
            _context = context;
            _votingRepository = votingRepository;
        }

        /// <summary>
        /// Get list of votings for person [person]
        /// </summary>
        /// <param name="onlyOpened">Returns only active votings</param>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [Authorize(AuthenticationSchemes = "person")]
        [HttpGet("/votings")]
        [ProducesResponseType(typeof(Page<VotingListItem>), 200)]
        public async Task<ActionResult<Page<VotingListItem>>> GetVotingsPerson(
            CancellationToken cancellationToken,
            [FromQuery] DefaultListBinding binding,
            [FromQuery] bool onlyOpened = false
            )
        {
            var house = await _houseRepository.GetByAddress(User.GetAddress(), cancellationToken);
            var person = await _personRepository.Get(User.GetId(), cancellationToken);

            var query = _context.Votings
                .AsNoTracking()
                .Include(o => o.Variants)
                    .ThenInclude(variant => variant.Votes)
                        .ThenInclude(vote => vote.Person)
                .Include(o => o.House)
                .Where(o => o.House.Id == house.Id)
                .Where(o => onlyOpened == true ? o.IsClosed == false : true)
                .Select(o => new VotingListItem
                {
                    Id = o.Id,
                    Title = o.Title,
                    IsClosed = o.IsClosed,
                    Variants = o.Variants.Select(variant => new VariantView
                    {
                        Id = variant.Id,
                        Title = variant.Title,
                        Count = variant.Votes.Count,
                        Votes = variant.Votes.Select(vote => new VoteView
                        {
                            Id = vote.Id,
                            Person = new PersonView
                            {
                                Id = vote.Person.Id,
                                Name = vote.Person.Name,
                                Surname = vote.Person.Surname
                            }
                        }).ToList()
                    }).ToList()
                });

            var items = await query
                .Skip(binding.Offset)
                .Take(binding.Limit)
                .ToListAsync();

            foreach (var item in items)
            {
                item.IsVoted = await IsVoted(person.Id, item.Id);
            }

            return new Page<VotingListItem>
            {
                Limit = binding.Limit,
                Offset = binding.Offset,
                Total = await query.CountAsync(),
                Items = items
            };
        }

        /// <summary>
        /// Get list of votings for admin [admin]
        /// </summary>
        /// <param name="onlyOpened">Returns only active votings</param>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpGet("/house/votings")]
        [ProducesResponseType(typeof(Page<VotingListItem>), 200)]
        public async Task<ActionResult<Page<VotingListItem>>> GetVotingsAdmin(
            CancellationToken cancellationToken,
            [FromQuery] DefaultListBinding binding,
            [FromQuery] bool onlyOpened = false
            )
        {
            var house = await _houseRepository.GetByAddress(User.GetAddress(), cancellationToken);

            var query = _context.Votings
                .AsNoTracking()
                .Include(o => o.Variants)
                    .ThenInclude(variant => variant.Votes)
                        .ThenInclude(vote => vote.Person)
                .Include(o => o.House)
                .Where(o => o.House.Id == house.Id)
                .Where(o => onlyOpened == true ? o.IsClosed == false : true)
                .Select(o => new VotingListItem
                {
                    Id = o.Id,
                    Title = o.Title,
                    IsClosed = o.IsClosed,
                    Variants = o.Variants.Select(variant => new VariantView
                    {
                        Id = variant.Id,
                        Title = variant.Title,
                        Count = variant.Votes.Count,
                        Votes = variant.Votes.Select(vote => new VoteView
                        {
                            Id = vote.Id,
                            Person = new PersonView
                            {
                                Id = vote.Person.Id,
                                Name = vote.Person.Name,
                                Surname = vote.Person.Surname
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

        /// <summary>
        /// Create new voting [admin]
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [HttpPost("/votings")]
        [ProducesResponseType(typeof(VotingView), 200)]
        [Authorize(AuthenticationSchemes = "admin")]
        public async Task<ActionResult<VotingView>> Create(
            CancellationToken cancellationToken,
            [FromBody] CreateVotingBinding binding)
        {
            var house = await _houseRepository.GetByAddress(User.GetAddress(), cancellationToken);

            var voting = new Voting(Guid.NewGuid(), binding.Title, house);

            binding.Variants.ForEach(o =>
            {
                voting.AddVariant(new Variant(Guid.NewGuid(), o, voting));
            });

            await _votingRepository.Save(voting, cancellationToken);

            return Ok(new VotingView
            {
                Id = voting.Id,
                Title = voting.Title,
                IsClosed = voting.IsClosed,
                Variants = voting.Variants.Select(o => new VariantView
                {
                    Id = o.Id,
                    Title = o.Title
                }).ToList()
            });
        }

        /// <summary>
        /// Vote [person]
        /// </summary>
        /// <param name="votingId">Voting id for vote</param>
        /// <param name="variantId">Variant id for voting</param>
        /// <response code="200">Successfully</response>
        /// <response code="404">Voting or variant not found</response>
        /// <response code="409">Person already voted</response>
        [HttpPost("/votings/{votingId}")]
        [ProducesResponseType(typeof(VotingView), 200)]
        [Authorize(AuthenticationSchemes = "person")]
        public async Task<ActionResult<VotingView>> Vote(
            CancellationToken cancellationToken,
            [FromRoute] Guid votingId,
            [FromQuery] Guid variantId)
        {
            var voting = await _votingRepository.Get(votingId, cancellationToken);

            if (voting == null)
                return NotFound(votingId);

            var variant = await _context.Variants
                .Include(o => o.Votes)
                .SingleOrDefaultAsync(o => o.Id == variantId);

            if (variant == null)
                return NotFound(variantId);

            var person = await _personRepository.Get(User.GetId(), cancellationToken);

            if (await IsVoted(person.Id, voting.Id))
                return Conflict("Already voted!");

            voting.Vote(variant.Id, person);

            await _votingRepository.Save(voting, cancellationToken);

            return Ok(new VotingView
            {
                Id = voting.Id,
                Title = voting.Title,
                IsClosed = voting.IsClosed,
                Variants = voting.Variants.Select(o => new VariantView
                {
                    Id = o.Id,
                    Title = o.Title,
                    Count = variant.Votes.Count,
                    Votes = o.Votes.Select(v => new VoteView
                    {
                        Id = v.Id,
                        Person = new PersonView
                        {
                            Id = v.Person.Id,
                            Name = v.Person.Name,
                            Surname = v.Person.Surname
                        }
                    }).ToList()
                }).ToList()
            });
        }

        /// <summary>
        /// Close voting [admin]
        /// </summary>
        /// <param name="votingId">Voting id for closing</param>
        /// <response code="200">Successfully</response>
        /// <response code="409">Voting already closed</response>
        [HttpPost("/votings/close")]
        [ProducesResponseType(typeof(VotingView), 200)]
        [ProducesResponseType(409)]
        [Authorize(AuthenticationSchemes = "admin")]
        public async Task<ActionResult<VotingView>> CloseVoting(
            CancellationToken cancellationToken,
            [FromQuery] Guid votingId)
        {
            var voting = await _votingRepository.Get(votingId, cancellationToken);

            if (voting == null)
                return NotFound(votingId);

            if (voting.IsClosed)
                return Conflict(votingId);

            voting.Close();

            await _votingRepository.Save(voting, cancellationToken);

            return Ok(new VotingView
            {
                Id = voting.Id,
                Title = voting.Title,
                IsClosed = voting.IsClosed,
                Variants = voting.Variants.Select(o => new VariantView
                {
                    Id = o.Id,
                    Title = o.Title,
                    Count = o.Votes.Count,
                    Votes = o.Votes.Select(v => new VoteView
                    {
                        Id = v.Id,
                        Person = new PersonView
                        {
                            Id = v.Person.Id,
                            Name = v.Person.Name,
                            Surname = v.Person.Surname
                        }
                    }).ToList()
                }).ToList()
            });
        }

        private async Task<bool> IsVoted(Guid personId, Guid votingId)
        {
            var vote = await _context.Votes
                .Include(o => o.Person)
                .Include(o => o.Variant)
                .Where(o => o.Person.Id == personId && o.Variant.Voting.Id == votingId).AnyAsync();
            return vote;
        }
    }
}
