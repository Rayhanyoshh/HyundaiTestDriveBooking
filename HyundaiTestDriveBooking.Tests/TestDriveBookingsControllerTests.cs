using HyundaiTestDriveBooking.Controllers;
using HyundaiTestDriveBooking.Data;
using HyundaiTestDriveBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyundaiTestDriveBooking.Tests;

public class TestDriveBookingsControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly TestDriveBookingsController _controller;

    public TestDriveBookingsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);
        _controller = new TestDriveBookingsController(_context);

        // Seed database with test data
        _context.CarModels.Add(new CarModel { Brand = "Hyundai", Model = "Elantra", Year = 2021, Image = "elantra.jpg" });
        _context.SaveChanges();
    }

    [Fact]
    public async Task PostTestDriveBooking_CreatesNewBooking()
    {
        var newBooking = new TestDriveBooking
        {
            CarModelId = 1,
            PreferredDate = DateTime.Now.AddDays(1),
            TimeSlot = "10:00 AM",
            CustomerName = "John Doe",
            CustomerEmail = "johndoe@example.com",
            CustomerPhone = "1234567890"
        };

        var result = await _controller.PostTestDriveBooking(newBooking);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var booking = Assert.IsType<TestDriveBooking>(createdAtActionResult.Value);

        Assert.Equal("John Doe", booking.CustomerName);
    }

    // Add more tests for other methods...
}

