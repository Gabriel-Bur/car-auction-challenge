namespace BCA.Challenge.CarAuction.API.Request;

public class AddVehicleRequest
{
    public string Manufacturer { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal StartingBid { get; set; }
    public int? NumberOfDoors { get; set; }     
    public int? NumberOfSeats { get; set; }     
    public double? LoadCapacity { get; set; }     
}
