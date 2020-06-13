using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Models.Repository
{
    public sealed class NewsRepository : INewsRepository
    {
        private readonly FirstHackathonDbContext _context;
        public NewsRepository(FirstHackathonDbContext context)
        {
            _context = context;
        }

        public async Task<NewsPost> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.News.SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task Save(NewsPost newsPost, CancellationToken cancellationToken)
        {
            if (_context.Entry(newsPost).State == EntityState.Detached)
                _context.News.Add(newsPost);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
