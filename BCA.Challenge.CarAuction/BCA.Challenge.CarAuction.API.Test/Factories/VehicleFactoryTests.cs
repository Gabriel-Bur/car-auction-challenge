using BCA.Challenge.CarAuction.API.Factory;
using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Test.Factories;

public class VehicleFactoryTests
{
    [Fact]
    public void CreateVehicle_Hatchback_ReturnHatchback()
    {
        // Arrange
        var type = VehicleType.Hatchback;
        var manufacturer = "Toyota";
        var year = 2020;
        var startingBid = 10000m;
        var numberOfDoors = 4;

        // Act
        var vehicle = VehicleFactory.CreateVehicle(type, manufacturer, year, startingBid, numberOfDoors, null, null);

        // Assert
        Assert.IsType<Hatchback>(vehicle);
        Assert.Equal(manufacturer, vehicle.Manufacturer);
        Assert.Equal(year, vehicle.Year);
        Assert.Equal(startingBid, vehicle.StartingBid);
        Assert.Equal(numberOfDoors, ((Hatchback)vehicle).NumberOfDoors);
    }

    [Fact]
    public void CreateVehicle_Sedan_ReturnSedan()
    {
        // Arrange
        var type = VehicleType.Sedan;
        var manufacturer = "Honda";
        var year = 2021;
        var startingBid = 15000m;
        var numberOfDoors = 4;


        // Act
        var vehicle = VehicleFactory.CreateVehicle(type, manufacturer, year, startingBid, numberOfDoors, null, null);

        // Assert
        Assert.IsType<Sedan>(vehicle);
        Assert.Equal(manufacturer, vehicle.Manufacturer);
        Assert.Equal(year, vehicle.Year);
        Assert.Equal(startingBid, vehicle.StartingBid);
        Assert.Equal(numberOfDoors, ((Sedan)vehicle).NumberOfDoors);
    }

    [Fact]
    public void CreateVehicle_SUV_ReturnSUV()
    {
        // Arrange
        var type = VehicleType.SUV;
        var manufacturer = "Ford";
        var year = 2019;
        var startingBid = 20000m;
        var numberOfSeats = 7;

        // Act
        var vehicle = VehicleFactory.CreateVehicle(type, manufacturer, year, startingBid, null, numberOfSeats, null);

        // Assert
        Assert.IsType<SUV>(vehicle);
        Assert.Equal(manufacturer, vehicle.Manufacturer);
        Assert.Equal(year, vehicle.Year);
        Assert.Equal(startingBid, vehicle.StartingBid);
        Assert.Equal(numberOfSeats, ((SUV)vehicle).NumberOfSeats);
    }

    [Fact]
    public void CreateVehicle_Truck_ReturnTruck()
    {
        // Arrange
        var type = VehicleType.Truck;
        var manufacturer = "Volvo";
        var year = 2018;
        var startingBid = 30000m;
        var capacity = 15.5;

        // Act
        var vehicle = VehicleFactory.CreateVehicle(type, manufacturer, year, startingBid, null, null, capacity);

        // Assert
        Assert.IsType<Truck>(vehicle);
        Assert.Equal(manufacturer, vehicle.Manufacturer);
        Assert.Equal(year, vehicle.Year);
        Assert.Equal(startingBid, vehicle.StartingBid);
        Assert.Equal(capacity, ((Truck)vehicle).LoadCapacity);
    }

    [Fact]
    public void CreateVehicle_InvalidVehicleType_ThrowsArgumentException()
    {
        // Arrange
        var type = (VehicleType)999; // Invalid vehicle type
        var manufacturer = "Tesla";
        var year = 2022;
        var startingBid = 50000m;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => VehicleFactory.CreateVehicle(type, manufacturer, year, startingBid, null, null, null));
    }

}
