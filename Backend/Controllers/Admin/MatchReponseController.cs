using Backend.Dtos.MatchResponse;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[Route("api/match-response")]
[ApiController]
public class MatchReponseController : ControllerBase
{
    private readonly BongDa24HContext _context;
    private readonly IMatchResponseRepository _matchResponseRepository;
    public MatchReponseController(BongDa24HContext context, IMatchResponseRepository matchResponseRepository)
    {
        _context = context;
        _matchResponseRepository = matchResponseRepository;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateMatchResponse([FromBody] MatchResponseDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var matchResponse = await _matchResponseRepository.CreateMatchResponseAsyn(dto);
            if (matchResponse == null)
            {
                return BadRequest(new
                {
                    message = "Chấp nhận bắt đối không thành công.",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Tạo Phản hồi bắt đối thành công.",
                data = matchResponse,
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message

            });
        }
    }
    [Authorize]
    [HttpPut("{responseId}")]
    public async Task<IActionResult> UpdateStatusResponse([FromBody] MatchResponseDto dto, [FromRoute] int responseId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var matchResponse = await _matchResponseRepository.GetMatchResponseById(responseId);
            if (matchResponse == null)
            {

                return BadRequest(new
                {
                    message = "Không tìm thấy phản hồi bắt đối",
                    success = false
                });

            }
            matchResponse.Status = dto.Status;
            await _matchResponseRepository.UpdateMatchResponseAsyn(matchResponse);
            return Ok(new
            {
                message = "Cập nhật trạng thái thành công",
                data = matchResponse,
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message

            });
        }
    }

}