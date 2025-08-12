using Backend.Dtos;
using Backend.Models;

namespace Backend.Mappers;

public static class PromotionMapper
{
    public static Promotion ToCreatePromotion(this CreatePromotionDto createPromotionDto)
    {
        return new Promotion
        {
            DiscountCode = createPromotionDto.DiscountCode,
            DiscountPercent = createPromotionDto.DiscountPercent,
            ExpiryDate = createPromotionDto.ExpiryDate,
            ClusterId = createPromotionDto.ClusterId
        };
    }
}