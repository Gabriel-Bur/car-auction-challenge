using BCA.Challenge.CarAuction.API.Interfaces;
using BCA.Challenge.CarAuction.API.Services;

namespace BCA.Challenge.CarAuction.API.Endpoints;

public class AuctionEndpoints : IEndpoint
{
    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("api/v1/auction")
            .WithTags("Auction")
            .WithDescription("Everything about auction")
            .WithOpenApi();

        group.MapPost("/{vehicleId}/start", () => Console.Write("Teste"))
            .WithSummary("Start auction for specified vehicle");

        group.MapPost("/{vehicleId}/bid", () => Console.Write("Teste"))
            .WithSummary("Place a bid for specified vehicle");

        group.MapPost("/{vehicleId}/close", () => Console.Write("Teste"))
            .WithSummary("Close auction for specified vehicle");

    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IAuctionService, AuctionService>();
    }
}
