using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Models.Repository
{
    public sealed class MeetingRepository : IMeetingRepository
    {
        private readonly FirstHackathonDbContext _context;
        public MeetingRepository(FirstHackathonDbContext context)
        {
            _context = context;
        }

        public async Task<Meeting> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Meetings.SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task Save(Meeting meeting, CancellationToken cancellationToken)
        {
            if (_context.Entry(meeting).State == EntityState.Detached)
                _context.Meetings.Add(meeting);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Meeting> GetByHouseId(Guid houseId, CancellationToken cancellationToken)
        {
            return await _context.Meetings.Include(o => o.House)
                .SingleOrDefaultAsync(w => w.House.Id == houseId, cancellationToken);
        }
    }
}
