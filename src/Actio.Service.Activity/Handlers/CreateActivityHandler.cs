using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exception;
using Actio.Service.Activity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Service.Activity.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        
        private readonly IBusClient _busClient;

        private readonly IServiceScopeFactory _serviceScopeFactory;


        public CreateActivityHandler(IBusClient busClient,
            IServiceScopeFactory serviceScopeFactory)
        {
            _busClient = busClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating activity:{command.Category} {command.Name}");
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _activityService = scope.ServiceProvider.GetService<IActivityService>();
                    await _activityService.AddAsync(command.Id, command.UserId,
                        command.Category, command.Name, command.Description, command.CreatedAt);
                    /*await _busClient.PublishAsync(
                        new CreateActivity());*/
                }
       
                return;
            }

            catch (ActioException exception)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, exception.Code,
                    exception.Message));
            }
            
            catch (Exception exception)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, "Error",
                    exception.Message));
            }
        }
    }
}