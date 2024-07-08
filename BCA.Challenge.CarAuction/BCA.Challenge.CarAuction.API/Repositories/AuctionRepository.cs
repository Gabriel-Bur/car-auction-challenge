using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Models;
using System.Collections.Concurrent;

namespace BCA.Challenge.CarAuction.API.Repositories;

public class AuctionRepository : IAuctionRepository
{
    private readonly ConcurrentDictionary<Guid, Auction> _auctions = new();

    public Task<bool> AddAuctionAsync(Auction auction)
    {
        return Task.FromResult(_auctions.TryAdd(auction.VehicleId, auction));
    }

    public Task<bool> AuctionExistsAsync(Guid vehicleId)
    {
        return Task.FromResult(_auctions.ContainsKey(vehicleId));
    }

    public Task<Auction?> GetAuctionAsync(Guid vehicleId)
    {
        _auctions.TryGetValue(vehicleId, out Auction? auction);
        return Task.FromResult(auction);
    }

    public Task<bool> UpdateAuctionAsync(Auction auction)
    {
        _auctions[auction.VehicleId] = auction;
        return Task.FromResult(true);
    }
}
