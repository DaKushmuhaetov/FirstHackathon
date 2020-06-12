using FirstHackathon.Models.Votes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Context.Repository
{
    public interface IVotingRepository
    {
        Task<Voting> Get(Guid id, CancellationToken cancellationToken);
        Task Save(Voting voting, CancellationToken cancellationToken);
    }
}
