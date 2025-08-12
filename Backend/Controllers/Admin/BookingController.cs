using Backend.Dtos.Booking;
using Backend.Dtos.Payment;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[Route("api/booking")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly BongDa24HContext _context;
    private readonly IBookingRepository _bookingRepository;
    public BookingController(BongDa24HContext context, IBookingRepository bookingRepository)
    {
        _context = context;
        _bookingRepository = bookingRepository;
    }
    [HttpGet("pitchId")]
    public async Task<IActionResult> GetBookings(int pitchId)
    {
        try
        {
            var bookings = await _bookingRepository.GetBookingsAsync(pitchId);
            if (bookings == null || !bookings.Any())
            {
                return NotFound(new
                {
                    message = "Không tìm thấy đặt sân nào",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Lấy danh sách đặt sân thành công",
                data = bookings,
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message,
                success = false
            });
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBooking booking)
    {
        try
        {
            if (booking == null)
            {
                return BadRequest(new
                {
                    message = "Thông tin đặt sân không hợp lệ",
                    success = false
                });
            }
            var createBooking = await _bookingRepository.CreateBookingAsync(booking);
            return Ok(new
            {
                message = "Tạo đặt sân thành công",
                data = createBooking,
                success = true
            });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new
            {
                message = ex.Message,
                success = false
            });
        }
    }
    [HttpPut("{bookingId}")]
    public async Task<IActionResult> UpdateBooking([FromBody] CreateBooking booking, [FromRoute] int bookingId)
    {
        try
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (existingBooking == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy đặt sân",
                    success = false
                });
            }
            existingBooking.Status = booking.Status;
            await _bookingRepository.UpdateBookingAsync(existingBooking);
            return Ok(new
            {
                message = "Cập nhật đặt sân thành công",
                data = existingBooking,
                success = true
            });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new
            {
                message = ex.Message,
                success = false
            });
        }
    }
    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> DeleteBooking([FromRoute] int bookingId)
    {
        try
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy đặt sân",
                    success = false
                });
            }
            var isDeleted = await _bookingRepository.DeleteBookingAsync(bookingId);
            if (!isDeleted)
            {
                return BadRequest(new
                {
                    message = "Xóa đặt sân không thành công",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Xóa đặt sân thành công",
                success = true
            });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new
            {
                message = ex.Message,
                success = false
            });
        }
    }
    [HttpPost("bookingId/deposit")]
    public async Task<IActionResult> DepositBooking([FromBody] CreatePaymentDto payment)
    {
        try
        {
            if (payment == null)
            {
                return BadRequest(new
                {
                    message = "Thông tin thanh toán không hợp lệ",
                    success = false
                });
            }
            // Lấy thông tin múi giờ của Việt Nam (UTC+7)
            TimeZoneInfo vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Chuyển thời gian UTC hiện tại sang giờ Việt Nam
            DateTime vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamZone);

            // Gán giờ Việt Nam vào payment.PaymentTime
            payment.PaymentTime = vnTime;
            await _bookingRepository.DepositeBookingAsync(payment);
            return Ok(new
            {
                message = "Đặt sân đã được đặt cọc thành công",
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message,
                success = false
            });
        }
    }
    [HttpPost("{bookingId}/cancel")]
    public async Task<IActionResult> CancelBooking([FromRoute] int bookingId)
    {
        try
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (existingBooking == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy đặt sân",
                    success = false
                });
            }
            if (existingBooking.IsDeposited)
            {
                return BadRequest(new
                {
                    message = "Không thể hủy đặt sân đã được đặt cọc",
                    success = false
                });
            }
            existingBooking.Status = 1;
            await _bookingRepository.UpdateBookingAsync(existingBooking);
            return Ok(new
            {
                message = "Đặt sân đã được hủy thành công",
                data = existingBooking,
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message,
                success = false
            });
        }
    }
}