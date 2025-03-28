﻿using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories;

public class UnitofWork(AppDbContext context) : IUnitofWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
