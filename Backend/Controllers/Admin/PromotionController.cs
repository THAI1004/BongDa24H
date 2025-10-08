using Backend.Dtos;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[Route("api/promotion")]
[ApiController]
public class PromotionController : ControllerBase
{
    private readonly IPromotionRepository _promotionRepository;
    public PromotionController(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePromotion(CreatePromotionDto createPromotionDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createPromotionDto == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu mã giảm giá không hợp lệ.",
                    success = false
                });
            }
            await _promotionRepository.CreatePromotionAsyn(createPromotionDto);
            return Ok(new
            {
                message = "Tạo mã giảm giá thành công.",
                data = createPromotionDto,
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