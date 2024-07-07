using BCA.Challenge.CarAuction.API.Interfaces;
using Microsoft.OpenApi.Models;

namespace BCA.Challenge.CarAuction.API.Endpoints
{
    public class SwaggerEndpoints : IEndpoint
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CarAuction",
                    Version = "v1",
                });
            });
        }

        public void MapEndpoints(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "CarAuctionAPI");
                });
            }
        }
    }
}
