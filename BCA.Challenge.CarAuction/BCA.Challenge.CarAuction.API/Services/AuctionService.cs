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
        if (!_auctions.ContainsKey(vehicleId) || !_auctions[vehicleId].IsActive)
        {
            throw new InvalidOperationException("Auction is not active.");
        }

        _auctions[vehicleId].IsActive = false;
    }

    public void PlaceBid(Guid vehicleId, decimal bidAmount, string bidder)
    {
        if (!_auctions.ContainsKey(vehicleId) || !_auctions[vehicleId].IsActive)
            throw new InvalidOperationException("Auction is not active.");

        if (bidAmount <= _auctions[vehicleId].CurrentBid)
            throw new ArgumentException("Bid amount must be greater than the current highest bid.");

        _auctions[vehicleId].CurrentBid = bidAmount;
        _auctions[vehicleId].CurrentBidder = bidder;
    }

    public IEnumerable<Vehicle> SearchVehicles(VehicleType? type, string? manufacturer, int? year)
    {
        var query = _vehicles.Values.AsQueryable();

        if (type is not null)
            query = query.Where(x => x.Model == type);

        if (!string.IsNullOrEmpty(manufacturer))
            query = query.Where(x => x.Manufacturer.Contains(manufacturer));

        if (year is not null)
            query = query.Where(x => x.Year.Equals(year));

        return query;
    }

    public void StartAuction(Guid vehicleId)
    {
        if (!_vehicles.ContainsKey(vehicleId))
            throw new KeyNotFoundException("Vehicle does not exist.");

        if (_auctions.ContainsKey(vehicleId) && _auctions[vehicleId].IsActive)
            throw new InvalidOperationException("Auction already active for this vehicle.");

        Auction newAuction = new() 
        {
            VehicleId = vehicleId,
            IsActive = true,
            CurrentBid = _vehicles[vehicleId].StartingBid,
            CurrentBidder = null 
        };

        if(!_auctions.TryAdd(vehicleId, newAuction))
            throw new Exception("Something went wrong when starting the auction.");

    }
}
