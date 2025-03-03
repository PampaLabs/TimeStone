using Microsoft.EntityFrameworkCore;

using TimeStone.Store.EntityFramework.Persistence;

namespace TimeStone.Store.EntityFramework;

/// <summary>
/// Provides methods for interacting with recurring tasks in the database.
/// Implements <see cref="IRecurrenceStore"/> to perform CRUD operations on recurring tasks.
/// </summary>
public class RecurrenceStore : IRecurrenceStore
{
    private readonly TimeStoneDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrenceStore"/> class.
    /// </summary>
    /// <param name="context">The <see cref="TimeStoneDbContext"/> instance used to interact with the database.</param>
    public RecurrenceStore(TimeStoneDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RecurringTask?> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        var entity = await _context.RecurringTasks
            .Where(x => x.Name == name)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) return null;

        return EntityConvert.FromEntity(entity);
    }

    /// <inheritdoc/>
    public async Task<RecurringTask?> GetAsync(string name, string target, CancellationToken cancellationToken = default)
    {
        var entity = await _context.RecurringTasks
            .Where(x => x.Name == name)
            .Where(x => x.Target == target)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) return null;

        return EntityConvert.FromEntity(entity);
    }

    /// <inheritdoc/>
    public async Task CreateAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        var entity = EntityConvert.ToEntity(item);
        await _context.RecurringTasks.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        _context.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        var entity = await _context.RecurringTasks
            .Where(x => x.Name == item.Name)
            .Where(x => x.Target == item.Target)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new Exception($"{nameof(RecurringTask)} not found.");
        }

        _context.RecurringTasks.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        _context.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(RecurringTask item, CancellationToken cancellationToken = default)
    {
        var entity = await _context.RecurringTasks
            .Where(x => x.Name == item.Name)
            .Where(x => x.Target == item.Target)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new Exception($"{nameof(RecurringTask)} not found.");
        }

        EntityConvert.ToEntity(item, entity);
        _context.RecurringTasks.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        _context.Entry(entity).State = EntityState.Detached;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<RecurringTask>> GetByStatusAsync(RecurringTaskStatus status, CancellationToken cancellationToken = default)
    {
        var result = _context.RecurringTasks
            .Where(x => x.Status == status)
            .AsNoTracking()
            .Select(EntityConvert.FromEntity)
            .ToList();

        return Task.FromResult<IEnumerable<RecurringTask>>(result);
    }
}
