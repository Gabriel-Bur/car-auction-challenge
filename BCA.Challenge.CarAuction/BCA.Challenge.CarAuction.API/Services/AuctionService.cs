using BCA.Challenge.CarAuction.API.Interfaces;
using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Services;

public class AuctionService : IAuctionService
{
    public AuctionService()
    {
            
    }
    public void AddVehicle(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public void CloseAuction(Guid vehicleId)
    {
        throw new NotImplementedException();
    }

    public void PlaceBid(Guid vehicleId, decimal bidAmount, string bidder)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Vehicle> SearchVehicles(string type, string manufacturer, string model, int year)
    {
        throw new NotImplementedException();
    }

    public void StartAuction(Guid vehicleId)
    {
        throw new NotImplementedException();
    }
}
