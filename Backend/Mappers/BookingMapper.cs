using Backend.Dtos.Booking;
using Backend.Models;

namespace Backend.Mappers;

public static class BookingMapper
{
    public static Booking ToBookingCreate(this CreateBooking createBooking)
    {
        return new Booking
        {
            UserId = createBooking.UserId,
            PitchId = createBooking.PitchId,
            BookingDate = createBooking.BookingDate,
            TimeSlot = createBooking.TimeSlot,
            Status = createBooking.Status,
            DepositAmount = createBooking.DepositAmount,
            IsDeposited = createBooking.IsDeposited
        };
    }
}