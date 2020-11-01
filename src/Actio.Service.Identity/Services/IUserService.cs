using System.Threading.Tasks;
using Actio.Common.Auth;

namespace Actio.Service.Identity.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);
        Task<JsonWebToken> LoginAsync(string email, string passowrd);
    }
}