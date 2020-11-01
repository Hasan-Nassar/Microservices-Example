using System;

namespace Actio.Service.Activity.Domain.Core
{
    public class Category
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        
        public Category()
        {
        }

        public Category(string name )
        {
            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
        }
    }
}