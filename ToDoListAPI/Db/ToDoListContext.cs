using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using ToDoListAPI.Entities;
using ToDoListAPI.Interfaces;

namespace ToDoListAPI.Db;

public class ToDoListContext : ApiAuthorizationDbContext<IdentityUser>
{
    private readonly ICurrentUserService _currentUserService;

    public ToDoListContext(
    DbContextOptions<ToDoListContext> options,
    IOptions<OperationalStoreOptions> operationOptions,
    ICurrentUserService currentUserService) : base(options, operationOptions)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Assignment> Assignments => Set<Assignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>().HasQueryFilter(r => r.OwnerId == Guid.Parse(_currentUserService.UserId!));
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<OwnedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.OwnerId = Guid.Parse(_currentUserService.UserId!);
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}