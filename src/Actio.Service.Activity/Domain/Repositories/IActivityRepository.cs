using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Framing.Impl;

namespace Actio.Service.Activity.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Core.Activity> GetAsync(Guid id);
        Task AddAsync(Core.Activity activity);
    }
}