using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[Route("api/review")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookingRepository _bookingRepository;
    public ReviewController(IReviewRepository reviewRepository, IBookingRepository bookingRepository)
    {
        _reviewRepository = reviewRepository;
        _bookingRepository = bookingRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewDto)
    {
        try
        {
            if (reviewDto == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu đánh giá không hợp lệ.",
                    success = false
                });
            }
            var booking = await _bookingRepository.GetBookingByIdAsync(reviewDto.BookingId);
            if (booking.Status != 3)
            {
                return BadRequest(new
                {
                    message = "Trận đấu chưa kết thúc bạn không thể đánh giá.",
                    success = false
                });
            }
            await _reviewRepository.CreateReviewAsyn(reviewDto);
            return Ok(new
            {
                message = "Thêm đánh giá thành công.",
                data = reviewDto,
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