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

        // Add ITimeController implementation provided with ICurentTimeProvider instance registered above
        services.AddSingleton<ITimeController, TimeController>(serviceProvider =>
        {
            ICurrentTimeProvider currentTimeProvider =
                serviceProvider.GetRequiredService<ICurrentTimeProvider>();

            return new TimeController(currentTimeProvider);
        });

        // Add ICommandTypeLibrary implementation
        services.AddSingleton<ICommandTemplateLibrary, CommandTemplateLibrary>();

        // Add ICommandLog implementation
        services.AddSingleton<ICommandLog, CommandLog>();

        return services;
    }
}
