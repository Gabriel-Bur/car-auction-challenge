using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Interfaces.Services;

public interface IAuctionService
{
    public Task StartAuctionAsync(Guid vehicleId);
    public Task PlaceBidAsync(Guid vehicleId, decimal bidAmount, string bidder);
    public Task CloseAuctionAsync(Guid vehicleId);
}
