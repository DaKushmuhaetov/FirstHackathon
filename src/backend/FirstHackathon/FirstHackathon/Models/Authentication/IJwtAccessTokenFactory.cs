using System.Threading;
using System.Threading.Tasks;

namespace FirstHackathon.Models.Authentication
{
    public interface IJwtAccessTokenFactory
    {
        Task<AccessToken> Create(Person person, CancellationToken cancellationToken);
        Task<AccessToken> Create(House house, CancellationToken cancellationToken);
    }
}
