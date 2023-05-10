﻿using GarageGenius.Modules.Users.Core.Entities;

namespace GarageGenius.Modules.Users.Core.Repositories;
internal interface IRoleRepository
{
    Task<Role> GetAsync(string name);
    Task<IReadOnlyList<Role>> GetAllAsync();
    Task AddAsync(Role role);
}
