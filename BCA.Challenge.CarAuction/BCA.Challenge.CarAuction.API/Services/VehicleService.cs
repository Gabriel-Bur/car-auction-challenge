using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Interfaces.Services;
using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        if (!await _vehicleRepository.AddVehicleAsync(vehicle))
            throw new ArgumentException("Vehicle with the same ID already exists.");
    }
    public async Task<IEnumerable<Vehicle>> SearchVehiclesAsync(VehicleType? type, string? manufacturer, int? year)
    {
        var query = await _vehicleRepository.GetAllVehiclesAsync();

        if (type is not null)
            query = query.Where(x => x.Model == type);

        if (!string.IsNullOrEmpty(manufacturer))
            query = query.Where(x => x.Manufacturer.Contains(manufacturer));

        if (year is not null)
            query = query.Where(x => x.Year.Equals(year));

        return query;
    }
}
