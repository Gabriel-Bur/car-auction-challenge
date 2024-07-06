namespace BCA.Challenge.CarAuction.API.Interfaces;

public interface IEndpoint
{
    void MapEndpoints(WebApplication app);
    void AddServices(IServiceCollection services);
}
