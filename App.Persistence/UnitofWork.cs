using App.Application.Contracts.Persistence;

namespace App.Persistence;

public class UnitofWork(AppDbContext context) : IUnitofWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
