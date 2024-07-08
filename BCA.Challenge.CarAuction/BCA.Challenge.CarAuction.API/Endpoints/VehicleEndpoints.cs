using BCA.Challenge.CarAuction.API.Factory;
using BCA.Challenge.CarAuction.API.Interfaces;
using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Interfaces.Services;
using BCA.Challenge.CarAuction.API.Models;
using BCA.Challenge.CarAuction.API.Repositories;
using BCA.Challenge.CarAuction.API.Request;
using BCA.Challenge.CarAuction.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCA.Challenge.CarAuction.API.Endpoints;

public class VehicleEndpoints : IEndpoint
{
    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("api/v1/vehicle")
            .WithTags("Vehicle")
            .WithDescription("Everything about vehicle")
            .WithOpenApi();

        group.MapGet("/", GetAllVehicles)
            .WithSummary("Retrieves filtered vehicles");

        group.MapPost("/", CreateVehicle)
            .WithSummary("Create a new car to be auctioned");
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IVehicleRepository, VehicleRepository>();
        services.AddSingleton<IVehicleService, VehicleService>();
    }

    internal async Task<IResult> GetAllVehicles(
        IVehicleService service, 
        [FromQuery] VehicleType? type, 
        [FromQuery] string? manufacture, 
        [FromQuery] int? year)
    {
        var result = await service.SearchVehiclesAsync(type, manufacture, year).ConfigureAwait(false);
        return result is not null ? Results.Ok(result) : Results.NotFound();
    }

    internal async Task<IResult> CreateVehicle(
        IVehicleService service, 
        [FromQuery] VehicleType type, 
        [FromBody] AddVehicleRequest addVehicle)
    {
        var vehicle = VehicleFactory.CreateVehicle(type, 
            addVehicle.Manufacturer, 
            addVehicle.Year, 
            addVehicle.StartingBid, 
            addVehicle.NumberOfDoors, 
            addVehicle.NumberOfSeats,
            addVehicle.LoadCapacity);
        
        await service.AddVehicleAsync(vehicle).ConfigureAwait(false);

        return Results.NoContent();
    }
}
