using BCA.Challenge.CarAuction.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsDefinitionServices();

var app = builder.Build();

app.UseEndpointDefinitionMapping();

app.UseHttpsRedirection();

app.Run();
