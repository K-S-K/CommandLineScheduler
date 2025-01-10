using CLS.Common.Times;
using CLS.Common.TimeControl;

namespace CLS.Service;

public static class DIExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentTimeProvider, CurrentTimeProvider>();

        // Add ITimeController implementation provided with ICurentTimeProvider instance registered above
        services.AddSingleton<ITimeController, TimeController>(serviceProvider =>
        {
            ICurrentTimeProvider currentTimeProvider = 
                serviceProvider.GetRequiredService<ICurrentTimeProvider>();

            return new TimeController(currentTimeProvider);
        });


        return services;
    }
}