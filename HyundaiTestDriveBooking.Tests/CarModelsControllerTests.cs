using HyundaiTestDriveBooking.Controllers;
using HyundaiTestDriveBooking.Data;
using HyundaiTestDriveBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HyundaiTestDriveBooking.Tests;

public class CarModelsControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CarModelsController _controller;

    public CarModelsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);
        _controller = new CarModelsController(_context);

        // Seed database with test data
        _context.CarModels.Add(new CarModel { Brand = "Hyundai", Model = "Elantra1", Year = 2021, Image = "elantra.jpg" });
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetCarModels_ReturnsAllCarModels()
    {
        var result = await _controller.GetListCarModels();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var carModels = Assert.IsType<List<CarModel>>(okResult.Value);

        Assert.Single(carModels);
    }

    // Add more tests for other methods...
}

