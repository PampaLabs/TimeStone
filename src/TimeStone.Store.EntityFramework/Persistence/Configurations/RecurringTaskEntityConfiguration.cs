using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TimeStone.Store.EntityFramework.Persistence.Entities;

namespace TimeStone.Store.EntityFramework.Persistence.Configurations;

/// <summary>
/// Configures the entity <see cref="RecurringTaskEntity"/> for use with Entity Framework.
/// Implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </summary>
public class RecurringTaskEntityConfiguration : IEntityTypeConfiguration<RecurringTaskEntity>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="RecurringTaskEntity"/> entity.
    /// This method is called by the Entity Framework runtime when the model is being created.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<RecurringTaskEntity> builder)
    {
        builder.ToTable("RecurringTasks");

        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => new { x.Name, x.Target });

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.Target)
            .HasMaxLength(100);

        builder.Property(e => e.Cron)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.StartDate);

        builder.Property(x => x.FinishDate);

        builder.Property(x => x.LastExecution);

        builder.Property(x => x.NextExecution);

        builder.HasIndex(x => x.Status);

#if NET7_0_OR_GREATER
        builder.HasIndex(x => x.NextExecution)
            .IsDescending();
#else
        builder.HasIndex(x => x.NextExecution);
#endif
    }
}