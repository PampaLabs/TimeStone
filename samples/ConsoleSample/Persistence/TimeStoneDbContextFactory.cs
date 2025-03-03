using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using TimeStone.Store.EntityFramework.Persistence;

namespace ConsoleSample.Persistence;

public class TimeStoneDbContextFactory : IDesignTimeDbContextFactory<TimeStoneDbContext>
{
    public TimeStoneDbContext CreateDbContext(string[] args)
    {
        string? connectionString = null;

        if (args.Length > 0)
        {
            connectionString = args[0];
        }

        var optionsBuilder = new DbContextOptionsBuilder<TimeStoneDbContext>();

        if (connectionString == null)
        {
            optionsBuilder.UseSqlServer(options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        }
        else
        {
            optionsBuilder.UseSqlServer(connectionString!, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        }

        return new TimeStoneDbContext(optionsBuilder.Options);
    }
}
