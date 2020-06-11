using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Models.Repository
{
    public sealed class PersonRepository : IPersonRepository
    {
        private readonly FirstHackathonDbContext _context;
        public PersonRepository(FirstHackathonDbContext context)
        {
            _context = context;
        }

        public async Task<Person> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.People.SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task<Person> GetByLogin(string login, CancellationToken cancellationToken)
        {
            return await _context.People.SingleOrDefaultAsync(w => w.Login == login, cancellationToken);
        }

        public async Task Save(Person person, CancellationToken cancellationToken)
        {
            if (_context.Entry(person).State == EntityState.Detached)
                _context.People.Add(person);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
