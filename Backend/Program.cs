using Backend.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Interfaces;
using Backend.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPitchClusterRepository, PitchClusterRepository>();
builder.Services.AddScoped<IPitchRepository, PitchRepository>();
builder.Services.AddScoped<IPricingRepository, PricingRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IMatchRequestRepository, MatchRequestRepository>();
builder.Services.AddScoped<IMatchResponseRepository, MatchResponseRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BongDa24HContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

