using Backend.Dtos;

namespace Backend.Interfaces;

public interface IPromotionRepository
{
    Task<CreatePromotionDto> CreatePromotionAsyn(CreatePromotionDto createPromotionDto);
}