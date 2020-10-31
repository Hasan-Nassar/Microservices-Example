using System;

namespace Actio.Common.Events
{
    public class ActivityCreated: IAthenticatedEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        protected ActivityCreated()
        {
        }

        public ActivityCreated(Guid id,Guid userid, string category,string name)
        {
            Id = id;
            userid = UserId;
            category = Category;
            Name = name;

        }
        
    }
}