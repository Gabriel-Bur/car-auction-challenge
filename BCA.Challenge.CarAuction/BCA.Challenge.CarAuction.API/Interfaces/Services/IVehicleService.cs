using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Interfaces.Services;

public interface IVehicleService
{
    public Task AddVehicleAsync(Vehicle vehicle);
    public Task<IEnumerable<Vehicle>> SearchVehiclesAsync(VehicleType? type, string? manufacturer, int? year);
}
