using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Models;
using System.Collections.Concurrent;

namespace BCA.Challenge.CarAuction.API.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ConcurrentDictionary<Guid, Vehicle> _vehicles = new();

    public Task<bool> AddVehicleAsync(Vehicle vehicle)
    {
        return Task.FromResult(_vehicles.TryAdd(vehicle.Id, vehicle));
    }

    public Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return Task.FromResult<IEnumerable<Vehicle>>(_vehicles.Values);
    }

    public Task<Vehicle?> GetVehicleAsync(Guid id)
    {
        _vehicles.TryGetValue(id, out Vehicle? vehicle);
        return Task.FromResult(vehicle);
    }
    public Task<bool> VehicleExistsAsync(Guid id)
    {
        return Task.FromResult(_vehicles.ContainsKey(id));
    }
}
