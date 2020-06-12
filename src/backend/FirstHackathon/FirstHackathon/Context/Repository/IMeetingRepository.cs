using FirstHackathon.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Context.Repository
{
    public interface IMeetingRepository
    {
        Task<Meeting> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Meeting meeting, CancellationToken cancellationToken);
        Task<Meeting> GetByHouseId(Guid idHouse, CancellationToken cancellationToken);
    }
}
