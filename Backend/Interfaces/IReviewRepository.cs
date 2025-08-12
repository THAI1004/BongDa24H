using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces;

public interface IReviewRepository
{
    Task<ReviewDto> CreateReviewAsyn(ReviewDto reviewDto);
}