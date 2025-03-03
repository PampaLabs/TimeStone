using Microsoft.Extensions.DependencyInjection;

namespace TimeStone;

/// <summary>
/// A builder class for configuring and adding services to an <see cref="IServiceCollection"/>.
/// This class facilitates the setup of dependencies for the TimeStone framework.
/// </summary>
public class TimeStoneBuilder
{
    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> where services are registered.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeStoneBuilder"/> class.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services into.</param>
    public TimeStoneBuilder(IServiceCollection services)
    {
        Services = services;
    }
}

