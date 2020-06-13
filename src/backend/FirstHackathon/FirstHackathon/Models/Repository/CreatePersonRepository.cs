using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Models.Repository
{
    public sealed class CreatePersonRepository : ICreatePersonRepository
    {
        private readonly FirstHackathonDbContext _context;
        private readonly IPersonRepository _personRepository;
        public CreatePersonRepository(
            FirstHackathonDbContext context,
            IPersonRepository personRepository)
        {
            _context = context;
            _personRepository = personRepository;
        }

        public async Task<CreatePersonRequest> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.CreatePersonRequests
                .Include(o => o.House)
                .SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task Save(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            if (_context.Entry(request).State == EntityState.Detached)
                _context.CreatePersonRequests.Add(request);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Create(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var existsPerson = await _personRepository.GetByLogin(request.Login, cancellationToken);
            if (existsPerson != null)
                throw new InvalidOperationException("Person with this login already registered");

            var existsRequest = await _context.CreatePersonRequests.SingleOrDefaultAsync(o => o.Login == request.Login, cancellationToken);
            if (existsRequest != null)
                throw new InvalidOperationException("Request with this login already exists");

            if (_context.Entry(request).State == EntityState.Detached)
                _context.CreatePersonRequests.Add(request);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Accept(Guid id, CancellationToken cancellationToken)
        {
            var request = await Get(id, cancellationToken);

            var person = new Person(request.Id, request.Name, request.Surname, request.Login, request.Password, request.House);

            await _personRepository.Save(person, cancellationToken);

            _context.CreatePersonRequests.Remove(request);

            await _context.SaveChangesAsync();
        }

        public async Task Reject(Guid id, CancellationToken cancellationToken)
        {
            var request = await Get(id, cancellationToken);

            _context.CreatePersonRequests.Remove(request);

            await _context.SaveChangesAsync();
        }
    }
}
