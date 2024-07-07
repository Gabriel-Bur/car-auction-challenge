using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Factory;

public static class VehicleFactory
{
    public static Vehicle CreateVehicle(
        VehicleType type, 
        string manufacturer,
        int year,
        decimal startingBig,
        int? numberOfDoors,
        int? numberOfSeats, 
        double? capacity) => type switch
    {
        VehicleType.Hatchback => new Hatchback()
        {
            NumberOfDoors = Convert.ToInt32(numberOfDoors),
            Manufacturer = manufacturer,
            Year = year,
            StartingBid = startingBig
        },
        VehicleType.Sedan => new Sedan()
        {
            NumberOfDoors = Convert.ToInt32(numberOfDoors),
            Manufacturer = manufacturer,
            Year = year,
            StartingBid = startingBig
        },
        VehicleType.SUV => new SUV()
        {
            NumberOfSeats = Convert.ToInt32(numberOfSeats),
            Manufacturer = manufacturer,
            Year = year,
            StartingBid = startingBig
        },
        VehicleType.Truck => new Truck()
        {
            LoadCapacity = Convert.ToDouble(capacity),
            Manufacturer = manufacturer,
            Year = year,
            StartingBid = startingBig
        },
        _ => throw new ArgumentException("Invalid vehicle type")

    };
}