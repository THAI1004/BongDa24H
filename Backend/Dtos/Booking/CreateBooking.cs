namespace Backend.Dtos.Booking;

public class CreateBooking
{

    public int UserId { get; set; }

    public int PitchId { get; set; }

    public DateOnly BookingDate { get; set; }

    public string TimeSlot { get; set; } = null!;

    public int? DepositAmount { get; set; }

    public bool IsDeposited { get; set; }

    public int? Status { get; set; }

}