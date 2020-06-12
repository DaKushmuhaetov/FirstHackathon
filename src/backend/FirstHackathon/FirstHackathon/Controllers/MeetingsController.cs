using FirstHackathon.Bindings;
using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Extensions;
using FirstHackathon.Models;
using FirstHackathon.Models.Authentication;
using FirstHackathon.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Controllers
{
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IHouseRepository _houseRepository;

        public MeetingsController(
            IMeetingRepository meetingRepository, 
            IHouseRepository houseRepository
            )
        {
            _meetingRepository = meetingRepository;
            _houseRepository = houseRepository;
        }

        /// <summary>
        /// Create new meeting
        /// </summary>
        /// <response code="200">Successfully</response>
        /// 
        [Authorize(AuthenticationSchemes = "admin")]
        [HttpPost("/meetings/create")]
        [Consumes("application/octet-stream")]
        public async Task<IActionResult> Create(
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
    }
}