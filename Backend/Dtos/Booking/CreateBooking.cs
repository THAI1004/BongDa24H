using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Booking;

public class CreateBooking
{
    [Required(ErrorMessage = "Yêu cầu nhập UserId")]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập PitchId")]

    public int PitchId { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập ngày đặt sân")]
    public DateOnly BookingDate { get; set; }

    public string TimeSlot { get; set; } = null!;

    public int? DepositAmount { get; set; }

    public bool IsDeposited { get; set; }

    public int? Status { get; set; }

}