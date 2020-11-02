using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exception;
using Actio.Service.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Service.Identity.Handlers
{
    public class CreateUserHandlers : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<CreateUserHandlers> _logger; 
         
        public CreateUserHandlers(IBusClient busClient
             ,ILogger<CreateUserHandlers> logger,IServiceScopeFactory serviceScopeFactory)
        {
            _busClient = busClient;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation ($"Creating user:{command.Email} {command.Name}");
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _userService = scope.ServiceProvider.GetService<IUserService>();
                    await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                }

                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                return;
            }

            catch (ActioException exception)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, exception.Code,
                    exception.Message));
            }
            
            catch (Exception exception)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, "Error",
                    exception.Message));
            }
            
        }
    }
}