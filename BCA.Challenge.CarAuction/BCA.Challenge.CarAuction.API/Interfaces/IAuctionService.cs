using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Interfaces;

public interface IAuctionService
{
    public void AddVehicle(Vehicle vehicle);
    public IEnumerable<Vehicle> SearchVehicles(Vehicle vehicle);
    public void StartAuction(Guid vehicleId);
    public void PlaceBid(Guid vehicleId, decimal bidAmount, string bidder);
    public void CloseAuction(Guid vehicleId);
}
