using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[Route("api/pricingRule")]
public class PricingController : ControllerBase
{
    private readonly BongDa24HContext _context;
    private readonly IPricingRepository _pricingRepository;
    public PricingController(BongDa24HContext context, IPricingRepository pricingRepository)
    {

        _pricingRepository = pricingRepository;
        _context = context;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPrice(int PitchId)
    {
        try
        {
            var pricingRules = await _pricingRepository.GetPriceAsync(PitchId);
            if (pricingRules == null || !pricingRules.Any())
            {
                return NotFound(new
                {
                    message = "Không tìm thấy thông tin giá cho sân bóng này",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Lấy thông tin giá thành công",
                data = pricingRules,
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePrice([FromBody] PricingRule pricingRule)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pricingRule == null)
            {
                return BadRequest(new
                {
                    message = "Thông tin giá không hợp lệ",
                    success = false
                });
            }

            var createdPricingRule = await _pricingRepository.CreatePriceAsync(pricingRule);
            return CreatedAtAction(nameof(GetPrice), new { PitchId = createdPricingRule.PitchId }, new
            {
                message = "Tạo thông tin giá thành công",
                data = createdPricingRule,
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdatePrice([FromBody] PricingRule pricingRule)
    {
        try
        {
            if (pricingRule == null)
            {
                return BadRequest(new
                {
                    message = "Thông tin giá không hợp lệ",
                    success = false
                });
            }

            var isUpdated = await _pricingRepository.UpdatePriceAsync(pricingRule);
            if (!isUpdated)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy thông tin giá để cập nhật",
                    success = false
                });
            }

            return Ok(new
            {
                message = "Cập nhật thông tin giá thành công",
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [Authorize]
    [HttpDelete("{pricingId}")]
    public async Task<IActionResult> DeletePricing(int pricingId)
    {
        try
        {
            var isDeleted = await _pricingRepository.DeletePriceAsync(pricingId);
            if (!isDeleted)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy thông tin giá sân để xóa.",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Xóa thông tin giá sân thành công.",
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}