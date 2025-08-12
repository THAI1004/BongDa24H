using Backend.Dtos.Booking;
using Backend.Dtos.Payment;
using Backend.Models;

namespace Backend.Interfaces;

public interface IBookingRepository
{
    // Define methods for booking operations
    Task<List<Booking>> GetBookingsAsync(int pitchId);
    Task<CreateBooking> CreateBookingAsync(CreateBooking booking);
    Task<bool> UpdateBookingAsync(Booking booking);
    Task<bool> DeleteBookingAsync(int bookingId);
    Task<Booking?> GetBookingByIdAsync(int bookingId);
    Task<Booking> DepositeBookingAsync(CreatePaymentDto payment);

}