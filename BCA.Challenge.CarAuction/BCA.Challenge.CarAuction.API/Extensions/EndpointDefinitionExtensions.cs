using BCA.Challenge.CarAuction.API.Interfaces;

namespace BCA.Challenge.CarAuction.API.Extensions;

public static class EndpointDefinitionExtensions
{
    public static void AddEndpointsDefinitionServices(this IServiceCollection services)
    {
        List<IEndpoint> endpointDefinitions = new();

        endpointDefinitions.AddRange(
            AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(x => typeof(IEndpoint).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>()
        );

        foreach (var endpoint in endpointDefinitions)
        {
            endpoint.AddServices(services);
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpoint>);
    }

    public static void UseEndpointDefinitionMapping(this WebApplication app)
    {
        var endpoitnsDefinition = app.Services.GetRequiredService<IReadOnlyCollection<IEndpoint>>();

        foreach (var endpoint in endpoitnsDefinition)
        {
            endpoint.MapEndpoints(app);
        }
    }
}
