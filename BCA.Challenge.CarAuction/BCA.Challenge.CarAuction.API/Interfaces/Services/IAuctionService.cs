using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Interfaces.Services;

public interface IAuctionService
{
    public Task AddVehicleAsync(Vehicle vehicle);
    public Task<IEnumerable<Vehicle>> SearchVehiclesAsync(VehicleType? type, string? manufacturer, int? year);
    public Task StartAuctionAsync(Guid vehicleId);
    public Task PlaceBidAsync(Guid vehicleId, decimal bidAmount, string bidder);
    public Task CloseAuctionAsync(Guid vehicleId);
}
