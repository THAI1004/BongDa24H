using Backend.Dtos.Payment;
using Backend.Models;

namespace Backend.Mappers;

public static class PaymentMapper
{
    public static Payment ToPaymentCreate(this CreatePaymentDto createPaymentDto)
    {
        return new Payment
        {
            BookingId = createPaymentDto.BookingId,
            Amount = createPaymentDto.Amount,
            Method = createPaymentDto.Method,
            Type = createPaymentDto.Type,
            PaymentTime = createPaymentDto.PaymentTime
        };
    }
}