﻿using System;
using System.Threading.Tasks;

namespace Actio.Service.Activity.Services
{
    public interface IActivityService
    {
        Task AddAsync(Guid id, Guid userId, string category,
            string name, string description, DateTime createdAt);
    }
}