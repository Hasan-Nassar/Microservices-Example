using System;
using System.Threading.Tasks;
using Actio.Common.Exception;
using Actio.Service.Activity.Domain.Core;
using Actio.Service.Activity.Domain.Repositories;

namespace Actio.Service.Activity.Services
{
    public class ActivityService: IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task AddAsync
            (Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activitycategory = new Category
            {
                Name = category,
                Id = Guid.NewGuid()
            };
            // if (activitycategory == null)
            // {
            //     throw new ActioException("category_not_found",$"Category:'{category}was not found.");
            // }
      
            var activity = new Domain.Core.Activity(id,activitycategory,userId,
                name , description,createdAt);
            await _activityRepository.AddAsync(activity);

        }
    }
}