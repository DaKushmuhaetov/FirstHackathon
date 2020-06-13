using FirstHackathon.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Context.Repository
{
    public interface ICreatePersonRepository
    {
        Task<CreatePersonRequest> Get(Guid id, CancellationToken cancellationToken);
        Task Save(CreatePersonRequest request, CancellationToken cancellationToken);
        Task Create(CreatePersonRequest request, CancellationToken cancellationToken);
        Task Accept(Guid id, CancellationToken cancellationToken);
        Task Reject(Guid id, CancellationToken cancellationToken);
    }
}
