using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Interfaces.Repositories;

public interface IVehicleRepository
{
    Task<bool> AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle?> GetVehicleAsync(Guid id);
    Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
    Task<bool> VehicleExistsAsync(Guid id);
}
