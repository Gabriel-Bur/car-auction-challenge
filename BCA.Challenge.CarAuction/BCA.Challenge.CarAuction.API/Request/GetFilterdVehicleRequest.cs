using BCA.Challenge.CarAuction.API.Models;

namespace BCA.Challenge.CarAuction.API.Request;

public class GetFilterdVehicleRequest
{
    public VehicleType? Type { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public int? Year { get; set; }

}
