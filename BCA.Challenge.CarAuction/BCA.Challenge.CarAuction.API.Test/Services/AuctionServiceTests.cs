using BCA.Challenge.CarAuction.API.Interfaces.Repositories;
using BCA.Challenge.CarAuction.API.Models;
using BCA.Challenge.CarAuction.API.Services;
using Moq;

namespace BCA.Challenge.CarAuction.API.Test.Services;

public class AuctionServiceTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
    private readonly AuctionService _auctionService;

    public AuctionServiceTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _auctionRepositoryMock = new Mock<IAuctionRepository>();
        _auctionService = new AuctionService(_vehicleRepositoryMock.Object, _auctionRepositoryMock.Object);
    }

    #region Close

    [Fact]
    public async Task WhenAuctionDoesNotExistOrIsNotActive_ThrowsInvalidOperationException()
    {
        var vehicleId = Guid.NewGuid();
        _auctionRepositoryMock.Setup(repo => repo.GetAuctionAsync(vehicleId))
            .ReturnsAsync(new Auction { IsActive = false });


        await Assert.ThrowsAsync<InvalidOperationException>(() => _auctionService.CloseAuctionAsync(vehicleId));
    }

    [Fact]
    public async Task _WhenAuctionIsActive_ShoudlRun_UpdatesAuction_Once()
    {
        var vehicleId = Guid.NewGuid();
        var auction = new Auction { IsActive = true };
        _auctionRepositoryMock.Setup(repo => repo.GetAuctionAsync(vehicleId))
            .ReturnsAsync(auction);

        await _auctionService.CloseAuctionAsync(vehicleId);

        _auctionRepositoryMock.Verify(repo => repo.UpdateAuctionAsync(auction), Times.Once);
    }

    #endregion

    #region Bid
    [Fact]
    public async Task WhenAuctionIsNotActive_ThrowsInvalidOperationException()
    {
        var vehicleId = Guid.NewGuid();
        _auctionRepositoryMock.Setup(repo => repo.GetAuctionAsync(vehicleId))
            .ReturnsAsync(new Auction { IsActive = false });

        await Assert.ThrowsAsync<InvalidOperationException>(() => _auctionService.PlaceBidAsync(vehicleId, 100, "BidderName"));
    }

    [Fact]
    public async Task WhenBidAmountIsNotGreaterThanCurrentBid_ThrowsArgumentException()
    {
        var vehicleId = Guid.NewGuid();
        _auctionRepositoryMock.Setup(repo => repo.GetAuctionAsync(vehicleId))
            .ReturnsAsync(new Auction { IsActive = true, CurrentBid = 100 });

        await Assert.ThrowsAsync<ArgumentException>(() => _auctionService.PlaceBidAsync(vehicleId, 50, "BidderName"));
    }

    [Fact]
    public async Task WhenAuctionHasValidBid_ShouldRun_UpdatesAuction_Once()
    {
        var vehicleId = Guid.NewGuid();
        var auction = new Auction { IsActive = true, CurrentBid = 100m };
        _auctionRepositoryMock.Setup(repo => repo.GetAuctionAsync(vehicleId))
            .ReturnsAsync(auction);


        await _auctionService.PlaceBidAsync(vehicleId, 150, "BidderName");


        _auctionRepositoryMock.Verify(repo => repo.UpdateAuctionAsync(auction), Times.Once);
        Assert.Equal(150, auction.CurrentBid);
        Assert.Equal("BidderName", auction.CurrentBidder);
    }
    #endregion

    #region Start
    [Fact]
    public async Task WhenVehicleDoesNotExist_ThrowsKeyNotFoundException()
    {
        var vehicleId = Guid.NewGuid();
        _vehicleRepositoryMock.Setup(repo => repo.GetVehicleAsync(vehicleId))
            .ReturnsAsync((Vehicle?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _auctionService.StartAuctionAsync(vehicleId));
    }

    [Fact]
    public async Task WhenAuctionIsAlreadyActive_ThrowsInvalidOperationException()
    {
        var vehicleId = Guid.NewGuid();
        _vehicleRepositoryMock.Setup(repo => repo.GetVehicleAsync(vehicleId))
            .ReturnsAsync(new Sedan { Id = vehicleId, StartingBid = 100 });

        _auctionRepositoryMock.Setup(repo => repo.GetAuctionAsync(vehicleId))
            .ReturnsAsync(new Auction { IsActive = true });

        await Assert.ThrowsAsync<InvalidOperationException>(() => _auctionService.StartAuctionAsync(vehicleId));
    }

    [Fact]
    public async Task WhenValidVehicleIdIsProvided_ShouldRun_StartsAuction_Once()
    {
        var vehicleId = Guid.NewGuid();
        var vehicle = new Sedan { Id = vehicleId, StartingBid = 100 };
        _vehicleRepositoryMock.Setup(repo => repo.GetVehicleAsync(vehicleId))

            .ReturnsAsync(vehicle);
        _auctionRepositoryMock.Setup(repo => repo.GetAuctionAsync(vehicleId))

            .ReturnsAsync((Auction?)null);

        await _auctionService.StartAuctionAsync(vehicleId);

        _auctionRepositoryMock.Verify(repo => repo.AddAuctionAsync(It.Is<Auction>(a => a.VehicleId == vehicleId && a.IsActive && a.CurrentBid == 100)), Times.Once);
    }
    #endregion
}
