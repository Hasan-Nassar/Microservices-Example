using System;
using Actio.Common.Exception;
using MongoDB.Bson.Serialization.Attributes;

namespace Actio.Service.Activity.Domain.Core
{
    public class Activity
    {
        
        [BsonId]
        public Guid Id { get;  set; }
        
        public string Name { get;  set; }
        
        public string Category { get;  set; }
        
        public string Description { get;  set; }
        
        public Guid UserId { get;  set; }
        
        public DateTime CreateAt { get;  set; }
        

        protected Activity()
        {
        }

        public Activity(Guid id ,Category category,Guid userId,string name ,string description,DateTime createAt)
        {
            // if (string.IsNullOrWhiteSpace(name))
            // {
            //     throw new ActioException("empty_activity_name",$"Activity name can not be empty.");
            // }
            
            Id = id;
             Category = category.Name;
            UserId = userId;
            Name = name;
            Description = description;
            CreateAt = createAt;
        }
    }
}