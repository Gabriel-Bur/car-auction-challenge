using BCA.Challenge.CarAuction.API.Extensions;
using BCA.Challenge.CarAuction.API.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsDefinitionServices();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.UseEndpointDefinitionMapping();

app.UseHttpsRedirection();

app.Run();
