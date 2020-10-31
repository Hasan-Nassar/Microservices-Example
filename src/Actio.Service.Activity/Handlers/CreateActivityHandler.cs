using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using RawRabbit;

namespace Actio.Service.Activity.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        /*public ILooger<CreateActivityHandler> _looger { get; }
        
        private readonly IActivityService _activityService;*/

        private readonly IBusClient _busClient;


        public CreateActivityHandler(IBusClient busClient
            /*IActivityService activityService,
            ILooger<CreateActivityHandler> logger*/)
        {
            /*_looger = looger;
            
            _activityService = activityService;*/

            _busClient = busClient;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating activity:{command.Category} {command.Name}");

            /*await _activityService.AddAsync(command.Id, command.UserId,
                command.Category, command.Name, command.Description, command.CreatedAt);*/
            await _busClient.PublishAsync(
                new ActivityCreated(command.Id, command.UserId, command.Category, command.Name));
            return;


            /*catch (ActioException exception)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, exception.Code,
                    exception.Message));
            }
            
            catch (Exception exception)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, "Error",
                    exception.Message));
            }*/

        }
    }
}