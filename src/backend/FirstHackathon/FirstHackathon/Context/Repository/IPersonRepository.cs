using FirstHackathon.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Context.Repository
{
    public interface IPersonRepository
    {
        Task<Person> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Person person);
    }
}
