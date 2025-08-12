using Backend.Dtos;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;

namespace Backend.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly BongDa24HContext _context;
    public ReviewRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<ReviewDto> CreateReviewAsyn(ReviewDto reviewDto)
    {
        _context.Reviews.Add(reviewDto.ToReview());
        await _context.SaveChangesAsync();
        return reviewDto;
    }
}