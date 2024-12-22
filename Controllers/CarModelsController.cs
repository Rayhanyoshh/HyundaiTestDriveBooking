using HyundaiTestDriveBooking.Data;
using HyundaiTestDriveBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyundaiTestDriveBooking.Controllers
{
   [ApiController]
[Route("api/[controller]")]
public class CarModelsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CarModelsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Metode GET untuk mendapatkan semua CarModel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarModel>>> GetListCarModels()
    {
        var carModels = await _context.CarModels.ToListAsync();
        return Ok(carModels);
    }

    // Metode GET untuk mendapatkan CarModel berdasarkan id
    [HttpGet("{id}")]
    public async Task<ActionResult<CarModel>> GetCarModel(int id)
    {
        var carModel = await _context.CarModels.FindAsync(id);

        if (carModel == null)
        {
            return NotFound();
        }

        return carModel;
    }

    // Metode POST untuk menambahkan CarModel baru
    [HttpPost]
    public async Task<ActionResult<CarModel>> PostCarModel(CarModel carModel)
    {
        _context.CarModels.Add(carModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCarModel", new { id = carModel.Id }, carModel);
    }

    // Metode PUT untuk memperbarui CarModel yang ada
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCarModel(int id, CarModel carModel)
    {
        if (id != carModel.Id)
        {
            return BadRequest();
        }

        _context.Entry(carModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CarModelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // Metode DELETE untuk menghapus CarModel
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCarModel(int id)
    {
        var carModel = await _context.CarModels.FindAsync(id);
        if (carModel == null)
        {
            return NotFound();
        }

        _context.CarModels.Remove(carModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CarModelExists(int id)
    {
        return _context.CarModels.Any(e => e.Id == id);
    }
}


}