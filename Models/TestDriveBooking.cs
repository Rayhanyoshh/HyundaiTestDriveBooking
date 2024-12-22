namespace HyundaiTestDriveBooking.Models;

public class TestDriveBooking
{
    public int Id { get; set; }
    public int CarModelId { get; set; }
    public CarModel CarModel { get; set; }
    public DateTime PreferredDate { get; set; }
    public string TimeSlot { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
}

