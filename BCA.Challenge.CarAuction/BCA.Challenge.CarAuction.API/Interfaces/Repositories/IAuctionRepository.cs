using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Interfaces.Repositories;

public interface IAuctionRepository
{
    Task<bool> AddAuctionAsync(Auction auction);
    Task<Auction?> GetAuctionAsync(Guid vehicleId);
    Task<bool> UpdateAuctionAsync(Auction auction);
}
