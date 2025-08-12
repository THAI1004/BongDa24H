namespace Backend.Dtos.Payment;

public class CreatePaymentDto
{
    public int BookingId { get; set; }

    public int Amount { get; set; }

    public DateTime PaymentTime { get; set; }

    public int? Type { get; set; }

    public int? Method { get; set; }
}
