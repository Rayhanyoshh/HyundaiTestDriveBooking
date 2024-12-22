using HyundaiTestDriveBooking.Data;
using HyundaiTestDriveBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TestDriveBookingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TestDriveBookingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<TestDriveBooking>> PostTestDriveBooking(TestDriveBooking testDriveBooking)
    {
        try
        {
            // Logging input data
            Console.WriteLine($"Received booking: CarModelId={testDriveBooking.CarModelId}, PreferredDate={testDriveBooking.PreferredDate}, TimeSlot={testDriveBooking.TimeSlot}, CustomerName={testDriveBooking.CustomerName}, CustomerEmail={testDriveBooking.CustomerEmail}, CustomerPhone={testDriveBooking.CustomerPhone}");

            if (testDriveBooking.CarModelId <= 0 || string.IsNullOrEmpty(testDriveBooking.CustomerName) ||
                string.IsNullOrEmpty(testDriveBooking.CustomerEmail) || string.IsNullOrEmpty(testDriveBooking.CustomerPhone) ||
                string.IsNullOrEmpty(testDriveBooking.TimeSlot))
            {
                return BadRequest(new { message = "Invalid input data" });
            }

            _context.TestDriveBookings.Add(testDriveBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestDriveBooking", new { id = testDriveBooking.Id }, testDriveBooking);
        }
        catch (Exception ex)
        {
            // Logging kesalahan
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }



    // Metode GET untuk mendapatkan daftar semua pemesanan test drive
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestDriveBooking>>> GetTestDriveBookings()
    {
        return await _context.TestDriveBookings
            .Include(b => b.CarModel)
            .ToListAsync();
    }

    private bool TestDriveBookingExists(int id)
    {
        return _context.TestDriveBookings.Any(e => e.Id == id);
    }
}