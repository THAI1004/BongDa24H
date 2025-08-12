using Backend.Dtos;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;

namespace Backend.Repository;

public class PromotionRepository : IPromotionRepository
{
    private readonly BongDa24HContext _context;
    public PromotionRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<CreatePromotionDto> CreatePromotionAsyn(CreatePromotionDto createPromotionDto)
    {
        _context.Promotions.Add(createPromotionDto.ToCreatePromotion());
        await _context.SaveChangesAsync();
        return createPromotionDto;
    }
}