using BCA.Challenge.CarAuction.API.Interfaces;
using BCA.Challenge.CarAuction.API.Models;
using System.Collections.Concurrent;

namespace BCA.Challenge.CarAuction.API.Services;

public class AuctionService : IAuctionService
{
    private readonly ConcurrentDictionary<Guid, Vehicle> _vehicles = new();
    private readonly ConcurrentDictionary<Guid, Auction> _auctions = new();

    public void AddVehicle(Vehicle vehicle)
    {
        if (!_vehicles.TryAdd(vehicle.Id, vehicle))
            throw new ArgumentException("Vehicle with the same ID already exists.");
        
    }

    public void CloseAuction(Guid vehicleId)
    {
        throw new NotImplementedException();
    }

    public void PlaceBid(Guid vehicleId, decimal bidAmount, string bidder)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Vehicle> SearchVehicles(VehicleType? type, string? manufacturer, int? year)
    {
        var query = _vehicles.Values.AsQueryable();

        if (type is not null)
            query.Where(x => x.Model == type);

        if (!string.IsNullOrEmpty(manufacturer))
            query.Where(x => x.Manufacturer.Equals(manufacturer));

        if (year is not null)
            query.Where(x => x.Year.Equals(year));

        return query;
    }

    public void StartAuction(Guid vehicleId)
    {
        throw new NotImplementedException();
    }
}
