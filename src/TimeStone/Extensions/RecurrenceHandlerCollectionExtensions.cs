using System.Reflection;

namespace TimeStone;

/// <summary>
/// Extension methods for <see cref="IRecurrenceHandlerCollection"/> that provide additional functionality
/// for managing recurrence handlers.
/// </summary>
public static class RecurrenceHandlerCollectionExtensions
{
    /// <summary>
    /// Builds an <see cref="IRecurrenceHandlerProvider"/> from the current collection of handlers.
    /// </summary>
    /// <param name="handlers">The collection of recurrence handlers to be used by the provider.</param>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve dependencies for handlers.</param>
    /// <returns>An <see cref="IRecurrenceHandlerProvider"/> that can provide handlers for recurrence tasks.</returns>
    public static IRecurrenceHandlerProvider BuildHandlerProvider(this IRecurrenceHandlerCollection handlers, IServiceProvider serviceProvider)
    {
        var activator = RecurrenceHandlerActivator.FromServiceProvider(serviceProvider);

        return new RecurrenceHandlerProvider(handlers, activator);
    }

    /// <summary>
    /// Registers all recurrence handlers from the specified assembly by scanning for types 
    /// that implement <see cref="IRecurrenceHandler"/> and are annotated with <see cref="RecurrenceHandlerAttribute"/>.
    /// </summary>
    /// <param name="handlers">The collection of recurrence handlers to register the handlers to.</param>
    /// <param name="assembly">The assembly to scan for handler types.</param>
    /// <returns>The updated <see cref="IRecurrenceHandlerCollection"/> with handlers registered.</returns>
    public static IRecurrenceHandlerCollection RegisterHandlersFromAssembly(this IRecurrenceHandlerCollection handlers, Assembly assembly)
    {
        var handlerType = typeof(IRecurrenceHandler);

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Contains(handlerType)).ToList();

        foreach (var type in types)
        {
            var attributes = type.GetCustomAttributes<RecurrenceHandlerAttribute>();

            foreach (var attribute in attributes)
            {
                handlers.AddHandler(attribute.Name, type);
            }
        }

        return handlers;
    }
}

