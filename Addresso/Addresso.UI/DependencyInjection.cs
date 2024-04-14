using System.Reflection;
using Addresso.UI.Api.Services;
using Fluxor;

namespace Addresso.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
        services.AddFluxor(options => 
            options
                .UseRouting()
                .ScanAssemblies(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}