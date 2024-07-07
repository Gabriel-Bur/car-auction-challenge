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

        group.MapPost("/{vehicleId}/start", StartAuction)
            .WithSummary("Start auction for specified vehicle");

        group.MapPost("/{vehicleId}/bid", PlaceBid)
            .WithSummary("Place a bid for specified vehicle");

        group.MapPost("/{vehicleId}/close", CloseAuction)
            .WithSummary("Close auction for specified vehicle");

    }

    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IAuctionService, AuctionService>();
    }

    internal IResult StartAuction(IAuctionService service, Guid vehicleId)
    {
        service.StartAuction(vehicleId);
        return Results.Ok();
    }

    internal IResult PlaceBid(IAuctionService service, Guid vehicleId, decimal bidAmount, string bidder)
    {
        service.PlaceBid(vehicleId, bidAmount, bidder);
        return Results.Ok();
    }

    internal IResult CloseAuction(IAuctionService service, Guid vehicleId)
    {
        service.CloseAuction(vehicleId);
        return Results.Ok();
    }

}
