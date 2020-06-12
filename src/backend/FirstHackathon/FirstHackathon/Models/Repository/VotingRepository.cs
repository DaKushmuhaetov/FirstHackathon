using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Models.Votes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Models.Repository
{
    public sealed class VotingRepository : IVotingRepository
    {
        private readonly FirstHackathonDbContext _context;
        public VotingRepository(FirstHackathonDbContext context)
        {
            _context = context;
        }

        public async Task<Voting> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Votings.SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task Save(Voting voting, CancellationToken cancellationToken)
        {
            if (_context.Entry(voting).State == EntityState.Detached)
                _context.Votings.Add(voting);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
