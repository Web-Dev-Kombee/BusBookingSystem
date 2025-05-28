using BusBookingSystem.Api.Middleware;
using BusBookingSystem.Application.Brokers;
using BusBookingSystem.Application.Handlers;
using BusBookingSystem.Application.Services;
using BusBookingSystem.Domain.Interfaces;
using BusBookingSystem.Infrastructure.Data;
using BusBookingSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Bus Booking System API",
        Version = "v1",
        Description = "API for managing bus bookings"
    });
});

builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IBusBroker, BusBroker>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<GetActiveBusesHandler>();
builder.Services.AddScoped<GetBusByNumberHandler>();
builder.Services.AddScoped<CreateBookingHandler>();
builder.Services.AddScoped<GetBookingHandler>();
builder.Services.AddScoped<UpdateBookingHandler>();
builder.Services.AddScoped<DeleteBookingHandler>();
builder.Services.AddScoped<GetAllBookingsHandler>();
builder.Services.AddScoped<GetBookingsCountHandler>();
builder.Services.AddScoped<GetSeatAvailabilityHandler>();
builder.Services.AddScoped<IBookingBroker, BookingBroker>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Add Logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", builder =>
    {
        builder.WithOrigins(
            "https://localhost:7022",
            "http://localhost:7022",
            "https://localhost:5001",
            "http://localhost:5001"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bus Booking System API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowWebApp");
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();