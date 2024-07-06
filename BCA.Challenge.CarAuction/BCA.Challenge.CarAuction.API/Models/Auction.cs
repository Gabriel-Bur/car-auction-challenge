namespace BCA.Challenge.CarAuction.API.Models;

public class Auction
{
    public Guid VehicleId { get; set; }
    public bool IsActive { get; set; }
    public decimal CurrentBid { get; set; }
    public string CurrentBidder { get; set; } = string.Empty;
}