namespace BCA.Challenge.CarAuction.API.Models;
public abstract class Vehicle
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public abstract string Model { get; }
    public int Year { get; set; }
    public decimal StartingBid { get; set; }
}
public class Sedan : Vehicle
{
    public int NumberOfDoors { get; set; }

    public override string Model => nameof(Sedan);
}

public class SUV : Vehicle
{
    public int NumberOfSeats { get; set; }

    public override string Model => nameof(SUV);
}

public class Truck : Vehicle
{
    public double LoadCapacity { get; set; }

    public override string Model => nameof(Truck);
}
