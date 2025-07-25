using CLS.Common.Times;
using CLS.Common.TimeControl;
using CLS.Common.CommandControl;
using CLS.Common.CommandLibrary;

namespace CLS.Service;

public static class DIExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Add ICurrentTimeProvider implementation
        services.AddSingleton<ICurrentTimeProvider, CurrentTimeProvider>();

        // Add ICommandTypeLibrary implementation
        services.AddSingleton<ICommandTemplateLibrary, CommandTemplateLibrary>();

        // Add ITimeController implementation provided with ICurrentTimeProvider instance registered above
        services.AddSingleton<ITimeController, TimeController>(serviceProvider =>
        {
            ICurrentTimeProvider currentTimeProvider =
                serviceProvider.GetRequiredService<ICurrentTimeProvider>();

            ILogger logger =
                serviceProvider.GetRequiredService<ILogger<TimeController>>();

            return new TimeController(currentTimeProvider, logger);
        });

        // Add ICommandLog implementation
        services.AddSingleton<ICommandLog, CommandLog>(serviceProvider =>
        {
            ILogger logger =
                serviceProvider.GetRequiredService<ILogger<CommandLog>>();

            return new CommandLog(logger);
        });

        return services;
    }
}
