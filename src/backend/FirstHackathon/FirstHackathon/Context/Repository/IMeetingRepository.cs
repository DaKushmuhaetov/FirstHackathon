using FirstHackathon.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Context.Repository
{
    public interface IMeetingRepository
    {
        Task<Meeting> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Meeting meeting, CancellationToken cancellationToken);
        Task<List<Meeting>> GetByHouseId(Guid houseId, CancellationToken cancellationToken);
    }
}
