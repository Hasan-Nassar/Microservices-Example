using System;
using System.Threading.Tasks;
using Actio.Service.Identity.Domain.Core;

namespace Actio.Service.Identity.Domain.Repository
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string name);
        Task  AddAsync(User user);
    }
}