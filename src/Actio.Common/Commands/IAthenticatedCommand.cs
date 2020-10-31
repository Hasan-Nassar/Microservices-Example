using System;

namespace Actio.Common.Commands
{
    public interface IAthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}