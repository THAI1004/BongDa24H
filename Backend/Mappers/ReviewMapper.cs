using Backend.Dtos;
using Backend.Models;

namespace Backend.Mappers;

public static class ReviewMapper
{
    public static Review ToReview(this ReviewDto reviewDto)
    {
        return new Review
        {
            BookingId = reviewDto.BookingId,
            PitchRating = reviewDto.PitchRating,
            OpponentRating = reviewDto.OpponentRating,
            OpponentSkill = reviewDto.OpponentSkill,
            Comment = reviewDto.Comment
        };
    }
}