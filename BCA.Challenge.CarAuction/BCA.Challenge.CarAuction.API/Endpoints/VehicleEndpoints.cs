using BCA.Challenge.CarAuction.API.Factory;
using BCA.Challenge.CarAuction.API.Interfaces;
using BCA.Challenge.CarAuction.API.Models;
using BCA.Challenge.CarAuction.API.Request;
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
    }


    internal IResult GetAllVehicles(
        IAuctionService service, 
        [FromQuery] VehicleType? type, 
        [FromQuery] string? manufacture, 
        [FromQuery] int? year)
    {
        var result = service.SearchVehicles(type, manufacture, year);
        return result.Any() ? Results.Ok(result) : Results.NotFound();
    }

    internal IResult CreateVehicle(
        IAuctionService service, 
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
        
        service.AddVehicle(vehicle);

        return Results.Ok();
    }
}
