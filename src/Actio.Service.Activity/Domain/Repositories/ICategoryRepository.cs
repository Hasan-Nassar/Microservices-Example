﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Service.Activity.Domain.Core;

namespace Actio.Service.Activity.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IEnumerable<Category>> BrowseAsync();
        Task AddAsync(Category category);
    }
}