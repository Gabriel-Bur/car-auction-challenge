using BCA.Challenge.CarAuction.API.Interfaces;

namespace BCA.Challenge.CarAuction.API.Endpoints;

public class VehicleEndpoints : IEndpoint
{
    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("api/v1/vehicle")
            .WithTags("Vehicle")
            .WithDescription("Everything about vehicle");

        group.MapGet("/", () => Console.Write("Teste"))
            .WithSummary("Retrieves filtered vehicles");

        group.MapPost("/", () => Console.Write("Teste"))
            .WithSummary("Create a new car to be auctioned");
    }

    public void AddServices(IServiceCollection services)
    {
    }
}
