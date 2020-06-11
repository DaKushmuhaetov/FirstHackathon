using FirstHackathon.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Context.Repository
{
    public interface IHouseRepository
    {
        Task<House> Get(Guid id, CancellationToken cancellationToken);
        Task Save(House house);
    }
}
