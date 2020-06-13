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
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly FirstHackathonDbContext _context;

        public MeetingsController(
            IMeetingRepository meetingRepository,
            IHouseRepository houseRepository,
            FirstHackathonDbContext context
            )
        {
            _meetingRepository = meetingRepository;
            _houseRepository = houseRepository;
            _context = context;
        }

        /// <summary>
        /// Create new meeting
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpPost("/meetings")]
        [ProducesResponseType(typeof(MeetingView), 200)]
        [Consumes("application/octet-stream")]
        public async Task<ActionResult<MeetingView>> Create(
            CancellationToken cancellationToken,
            [FromQuery] CreateMeetingBinding binding
            )
        {
            var address = User.GetAddress();
            House house = await _houseRepository.GetByAddress(address, cancellationToken);

            await using var ms = new MemoryStream();
            await Request.Body.CopyToAsync(ms, cancellationToken);

            var meeting = new Meeting(Guid.NewGuid(), binding.Title, binding.MeetingDate, binding.Description, ms.ToArray(), house);

            await _meetingRepository.Save(meeting, cancellationToken);

            return Ok(new MeetingView
            {
                Id = meeting.Id,
                Title = meeting.Title,
                MeetingDate = meeting.MeetingDate,
                Description = meeting.Description,
                House = new HouseView
                {
                    Id = house.Id,
                    Address = house.Address
                }
            });
        }

        /// <summary>
        /// Get meetings
        /// </summary>
        /// <param name="binding">Input model</param>
        /// <response code="200">Successfully</response>
        [Authorize(AuthenticationSchemes = "admin,person")]
        [HttpGet("/meetings")]
        [ProducesResponseType(typeof(Page<MeetingListItem>), 200)]
        public async Task<ActionResult<Page<MeetingListItem>>> GetMeetings(
            CancellationToken cancellationToken,
            [FromQuery] DateListBinding binding)
        {
            var address = User.GetAddress();
            House house = await _houseRepository.GetByAddress(address, cancellationToken);

            if (house == null)
                return NotFound(address);

            var query = _context.Meetings
                .AsNoTracking()
                .Include(o => o.House)
                .Where(o => o.House.Id == house.Id)
                .Select(o => new MeetingListItem
                {
                    Id = o.Id,
                    Title = o.Title,
                    MeetingDate = o.MeetingDate,
                    Description = o.Description
                });

            if (binding.StartDate != null)
                query = query.Where(o => o.MeetingDate >= binding.StartDate);
            if (binding.EndDate != null)
                query = query.Where(o => o.MeetingDate <= binding.EndDate);

            var items = await query
                .Skip(binding.Offset)
                .Take(binding.Limit)
                .ToListAsync();

            return new Page<MeetingListItem>
            {
                Limit = binding.Limit,
                Offset = binding.Offset,
                Total = await query.CountAsync(),
                Items = items
            };
        }
    }
}