using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Interfaces.Services;
using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Services;

public class AuctionService : IAuctionService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IAuctionRepository _auctionRepository;

    public AuctionService(
        IVehicleRepository vehicleRepository, 
        IAuctionRepository auctionRepository)
    {
        _vehicleRepository = vehicleRepository;
        _auctionRepository = auctionRepository;
    }

    public async Task CloseAuctionAsync(Guid vehicleId)
    {
        var auction = await _auctionRepository.GetAuctionAsync(vehicleId);
        if (auction is null || !auction.IsActive)
            throw new InvalidOperationException("Auction is not active.");

        auction.IsActive = true;
        await _auctionRepository.UpdateAuctionAsync(auction);
    }

    public async Task PlaceBidAsync(Guid vehicleId, decimal bidAmount, string bidder)
    {
        var auction = await _auctionRepository.GetAuctionAsync(vehicleId);

        if (auction == null || !auction.IsActive)
            throw new InvalidOperationException("Auction is not active.");

        if (bidAmount <= auction.CurrentBid)
            throw new ArgumentException("Bid amount must be greater than the current highest bid.");

        auction.CurrentBid = bidAmount;
        auction.CurrentBidder = bidder;

        await _auctionRepository.UpdateAuctionAsync(auction);
    }

    public async Task StartAuctionAsync(Guid vehicleId)
    {
        var GetVehicleTask = _vehicleRepository.GetVehicleAsync(vehicleId);
        var GetAuctionTask = _auctionRepository.GetAuctionAsync(vehicleId);

        await Task.WhenAll(GetVehicleTask, GetAuctionTask);

        Vehicle? vehicle = GetVehicleTask.Result;
        Auction? auction = GetAuctionTask.Result;

        if (vehicle is null)
            throw new KeyNotFoundException("Vehicle does not exist.");

        if (auction is not null && auction.IsActive)
            throw new InvalidOperationException("Auction already active for this vehicle.");

        Auction newAuction = new() 
        {
            VehicleId = vehicle.Id,
            IsActive = true,
            CurrentBid = vehicle.StartingBid,
            CurrentBidder = null 
        };

        await _auctionRepository.AddAuctionAsync(newAuction);
    }
}
