using FirstHackathon.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Context.Repository
{
    public interface INewsRepository
    {
        Task<NewsPost> Get(Guid id, CancellationToken cancellationToken);
        Task Save(NewsPost newsPost, CancellationToken cancellationToken);
    }
}
