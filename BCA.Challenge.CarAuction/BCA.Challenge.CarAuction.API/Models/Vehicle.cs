using System.Text.Json.Serialization;

namespace BCA.Challenge.CarAuction.API.Models;
public abstract class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Manufacturer { get; set; } = string.Empty;
    public abstract VehicleType Model { get; }
    public int Year { get; set; }
    public decimal StartingBid { get; set; }
}
public class Sedan : Vehicle
{
    public int NumberOfDoors { get; set; }

    public override VehicleType Model => VehicleType.Sedan;
}

public class Hatchback : Vehicle
{
    public int NumberOfDoors { get; set; }

    public override VehicleType Model => VehicleType.Hatchback;
}

public class SUV : Vehicle
{
    public int NumberOfSeats { get; set; }

    public override VehicleType Model => VehicleType.SUV;
}

public class Truck : Vehicle
{
    public double LoadCapacity { get; set; }

    public override VehicleType Model => VehicleType.Truck;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VehicleType
{
    Hatchback = 1,
    Sedan = 2,
    SUV = 3,
    Truck = 4
}