using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exception;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Service.Identity.Handlers
{
    public class CreateUserHandlers : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
       // private readonly IUserService _userService;
        private readonly ILogger<CreateUserHandlers> _logger; 
        // IUserService userService 
        public CreateUserHandlers(IBusClient busClient
             ,ILogger<CreateUserHandlers> logger)
        {
            _busClient = busClient;
         // _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation ($"Creating user:{command.Email} {command.Name}");
            try
            {
             //   await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                
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