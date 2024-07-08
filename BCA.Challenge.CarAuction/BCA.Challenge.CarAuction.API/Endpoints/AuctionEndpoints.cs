using BCA.Challenge.CarAuction.API.Interfaces;
using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Interfaces.Services;
using BCA.Challenge.CarAuction.API.Repositories;
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
        services.AddSingleton<IAuctionRepository, AuctionRepository>();
        services.AddSingleton<IAuctionService, AuctionService>();
    }

    internal async Task<IResult> StartAuction(IAuctionService service, Guid vehicleId)
    {
        await service.StartAuctionAsync(vehicleId).ConfigureAwait(false);
        return Results.Ok();
    }

    internal async Task<IResult> PlaceBid(IAuctionService service, Guid vehicleId, decimal bidAmount, string bidder)
    {
        await service.PlaceBidAsync(vehicleId, bidAmount, bidder).ConfigureAwait(false);
        return Results.Ok();
    }

    internal async Task<IResult> CloseAuction(IAuctionService service, Guid vehicleId)
    {
        await service.CloseAuctionAsync(vehicleId).ConfigureAwait(false);
        return Results.Ok();
    }

}
