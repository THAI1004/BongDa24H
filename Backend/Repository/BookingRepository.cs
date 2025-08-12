using Backend.Dtos.Booking;
using Backend.Dtos.Payment;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly BongDa24HContext _context;
    public BookingRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<CreateBooking> CreateBookingAsync(CreateBooking booking)
    {
        _context.Bookings.Add(booking.ToBookingCreate());
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> DeleteBookingAsync(int bookingId)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);
        if (booking == null)
        {
            return false;
        }
        _context.Bookings.Remove(booking);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Booking>> GetBookingsAsync(int pitchId)
    {
        return await _context.Bookings
         .Where(b => b.PitchId == pitchId).ToListAsync();
    }


    public async Task<bool> UpdateBookingAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Booking?> GetBookingByIdAsync(int bookingId)
    {
        return await _context.Bookings.FindAsync(bookingId);
    }
    public async Task<Booking> DepositeBookingAsync(CreatePaymentDto payment)
    {
        var booking = await _context.Bookings.FindAsync(payment.BookingId);
        if (booking == null)
        {
            throw new Exception("Booking not found");
        }
        _context.Payments.Add(payment.ToPaymentCreate());
        booking.Status = 2;
        booking.DepositAmount = payment.Amount; // Assuming you want to set the deposit amount
        booking.IsDeposited = true; // Assuming you want to mark the booking as deposited
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

}