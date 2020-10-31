using System;

namespace Actio.Common.Events
{
    public interface IAthenticatedEvent: IEvent
    {
        Guid UserId { get; set; }
        
    }
}