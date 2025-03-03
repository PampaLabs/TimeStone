using System.Reflection;

using Microsoft.EntityFrameworkCore;

using TimeStone.Store.EntityFramework.Persistence.Entities;

namespace TimeStone.Store.EntityFramework.Persistence;

/// <summary>
/// Represents the database context for the TimeStone system.
/// Inherits from <see cref="DbContext"/> and is used to interact with the database.
/// </summary>
public class TimeStoneDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the <see cref="DbSet{RecurringTaskEntity}"/> representing the recurring tasks in the database.
    /// </summary>
    public DbSet<RecurringTaskEntity> RecurringTasks { get; set; } = default!;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeStoneDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to configure the context, typically passed from the dependency injection container.</param>
    public TimeStoneDbContext(DbContextOptions<TimeStoneDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }
}
