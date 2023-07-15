using Microsoft.Extensions.DependencyInjection;

namespace Stravaig.Clock.DependencyInjection;

/// <summary>
/// Extensions to the IServiceCollection for adding the clock.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the clock singleton to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <returns>A reference to this instance of the IServiceCollection after
    /// this operation is completed.</returns>
    public static IServiceCollection AddClock(this IServiceCollection services)
    {
        return services.AddSingleton<IClock>(static _ => Clock.Instance);
    }
}