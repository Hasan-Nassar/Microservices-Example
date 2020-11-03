using System;
using System.Threading.Tasks;
using Actio.Service.Activity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Service.Activity.MongoDb
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }
       
        
        
        private IMongoCollection<Domain.Core.Activity> Collection
            => _database.GetCollection<Domain.Core.Activity>("Activity");
        
        
        public async Task<Domain.Core.Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x=>x.Id==id);

        public async Task AddAsync(Domain.Core.Activity activity)
            => await Collection.InsertOneAsync(activity);
        
        
    }
}