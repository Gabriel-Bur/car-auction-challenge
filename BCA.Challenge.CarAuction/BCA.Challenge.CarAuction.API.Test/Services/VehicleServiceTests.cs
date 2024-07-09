using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Models;
using BCA.Challenge.CarAuction.API.Services;
using Moq;

namespace BCA.Challenge.CarAuction.API.Test.Services;

public class VehicleServiceTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly VehicleService _vehicleService;

    public VehicleServiceTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _vehicleService = new VehicleService(_vehicleRepositoryMock.Object);
    }

    internal List<Vehicle> GetPredefinedSetOfVehicles()
    {
        return new List<Vehicle>
        {
            new SUV { Id = Guid.NewGuid(), Manufacturer = "Jeep", Year = 2023, StartingBid = 10000m, NumberOfSeats = 5 },
            new Sedan { Id = Guid.NewGuid(), Manufacturer = "Toyota", Year = 2020, StartingBid = 20000m, NumberOfDoors = 4 },
            new Truck { Id = Guid.NewGuid(), Manufacturer = "Ford", Year = 2018, StartingBid = 8500m, LoadCapacity = 400},
            new Hatchback { Id = Guid.NewGuid(), Manufacturer = "Fiat", Year = 2021, StartingBid = 0m, NumberOfDoors = 4 }
        };
    }

    #region Add

    [Fact]
    public async Task WhenAddingAnyVehicle_ShouldBySuccessfull()
    {
        // Arrange
        var vehicle = new Sedan { Id = Guid.NewGuid() };
        _vehicleRepositoryMock.Setup(repo => repo.AddVehicleAsync(vehicle))
            .ReturnsAsync(true);

        // Act
        await _vehicleService.AddVehicleAsync(vehicle);

        // Assert
        _vehicleRepositoryMock.Verify(repo => repo.AddVehicleAsync(vehicle), Times.Once);
    }

    [Fact]
    public async Task WhenAddingAnyVehicle_ThatAlreadyExists_ThrowsArgumentException()
    {
        // Arrange
        var vehicle = new Sedan { Id = Guid.NewGuid() };
        _vehicleRepositoryMock.Setup(repo => repo.AddVehicleAsync(vehicle))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _vehicleService.AddVehicleAsync(vehicle));
    }

    #endregion

    #region Search
    [Fact]
    public async Task WhenSearchVehicles_WithNoFilters_ShouldReturnAllVehicles()
    {
        // Arrange
        List<Vehicle> vehicles = GetPredefinedSetOfVehicles();

        _vehicleRepositoryMock.Setup(repo => repo.GetAllVehiclesAsync())
            .ReturnsAsync(vehicles);

        // Act
        var result = await _vehicleService.SearchVehiclesAsync(null, null, null);

        // Assert
        Assert.Equal(vehicles, result);
        _vehicleRepositoryMock.Verify(x => x.GetAllVehiclesAsync(), Times.Once);
    }

    [Fact]
    public async Task WhenSearchVehicles_FilterByType_ShouldReturnFilteredVehicles()
    {
        // Arrange
        var vehicleTypeFilter = VehicleType.Sedan;
        List<Vehicle> vehicles = GetPredefinedSetOfVehicles();
        _vehicleRepositoryMock.Setup(repo => repo.GetAllVehiclesAsync())
            .ReturnsAsync(vehicles);

        // Act
        var result = await _vehicleService.SearchVehiclesAsync(vehicleTypeFilter, null, null);

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains(result, x => x.Model.Equals(vehicleTypeFilter));
        _vehicleRepositoryMock.Verify(x => x.GetAllVehiclesAsync(), Times.Once);
    }

    [Fact]
    public async Task WhenSearchVehicles_FilterByManufacturer_ShouldReturnFilteredVehicles()
    {
        // Arrange
        var vehileManufacturerFilter = "Ford";
        List<Vehicle> vehicles = GetPredefinedSetOfVehicles();
        _vehicleRepositoryMock.Setup(repo => repo.GetAllVehiclesAsync())
            .ReturnsAsync(vehicles);

        // Act
        var result = await _vehicleService.SearchVehiclesAsync(null, vehileManufacturerFilter, null);

        Assert.NotEmpty(result);
        Assert.Contains(result, x => x.Manufacturer.Equals(vehileManufacturerFilter));
        _vehicleRepositoryMock.Verify(x => x.GetAllVehiclesAsync(), Times.Once);
    }

    [Fact]
    public async Task WhenSearchVehicles_FilterByYear_ShouldReturnFilteredVehicles()
    {
        // Arrange
        var vehicleYearFilter = 2020;
        List<Vehicle> vehicles = GetPredefinedSetOfVehicles();
        _vehicleRepositoryMock.Setup(repo => repo.GetAllVehiclesAsync())
            .ReturnsAsync(vehicles);

        // Act
        var result = await _vehicleService.SearchVehiclesAsync(null, null, vehicleYearFilter);

        Assert.NotEmpty(result);
        Assert.Contains(result, x => x.Year.Equals(vehicleYearFilter));
        _vehicleRepositoryMock.Verify(x => x.GetAllVehiclesAsync(), Times.Once);
    }

    [Fact]
    public async Task WhenSearchVehicles_FilteredByMultipleCriteria_ShouldReturnFilteredVehicles()
    {
        // Arrange
        var vehicleTypeFilter = VehicleType.Sedan;
        var vehileManufacturerFilter = "Toyota";
        var vehicleYearFilter = 2020;
        List<Vehicle> vehicles = GetPredefinedSetOfVehicles();
        _vehicleRepositoryMock.Setup(repo => repo.GetAllVehiclesAsync())
            .ReturnsAsync(vehicles);

        // Act
        var result = await _vehicleService.SearchVehiclesAsync(vehicleTypeFilter, vehileManufacturerFilter, vehicleYearFilter);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(vehicleTypeFilter, result.First().Model);
        Assert.Equal(vehileManufacturerFilter, result.First().Manufacturer);
        Assert.Equal(vehicleYearFilter, result.First().Year);
        _vehicleRepositoryMock.Verify(x => x.GetAllVehiclesAsync(), Times.Once);
    }


    [Fact]
    public async Task WhenSearchVehicles_FilteredByMultipleCriteria_WithoutMatches_ShouldReturnNoVehicles()
    {
        // Arrange
        var vehicleTypeFilter = VehicleType.Sedan;
        var vehicleYearFilter = 1985;

        List<Vehicle> vehicles = GetPredefinedSetOfVehicles();
        _vehicleRepositoryMock.Setup(repo => repo.GetAllVehiclesAsync())
            .ReturnsAsync(vehicles);

        // Act
        var result = await _vehicleService.SearchVehiclesAsync(vehicleTypeFilter, null, vehicleYearFilter);

        // Assert
        Assert.Empty(result);
        _vehicleRepositoryMock.Verify(x => x.GetAllVehiclesAsync(), Times.Once);
    }

    #endregion
}
