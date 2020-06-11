using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Models.Repository
{
    public sealed class HouseRepository : IHouseRepository
    {
        private readonly FirstHackathonDbContext _context;
        public HouseRepository(FirstHackathonDbContext context)
        {
            _context = context;
        }

        public async Task<House> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Houses.SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task Save(House house)
        {
            if (_context.Entry(house).State == EntityState.Detached)
                _context.Houses.Add(house);

            await _context.SaveChangesAsync();
        }
    }
}
