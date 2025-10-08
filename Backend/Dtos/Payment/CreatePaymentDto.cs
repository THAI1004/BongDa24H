using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Payment;

public class CreatePaymentDto
{
    [Required(ErrorMessage = "Yêu cầu nhập ID booking.")]
    public int BookingId { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập số tiền.")]

    public int Amount { get; set; }

    public DateTime PaymentTime { get; set; }

    public int? Type { get; set; }

    public int? Method { get; set; }
}
