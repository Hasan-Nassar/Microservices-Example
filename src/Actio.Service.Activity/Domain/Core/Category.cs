using System;

namespace Actio.Service.Activity.Domain.Core
{
    public class Category
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        
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