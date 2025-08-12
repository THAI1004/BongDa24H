using Backend.Dtos.MatchRequest;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Admin.Controllers;

[Route("api/match-request")]
public class MatchRequestController : ControllerBase
{
    private readonly BongDa24HContext _context;
    private readonly IMatchRequestRepository _matchRequestRepository;
    public MatchRequestController(BongDa24HContext context, IMatchRequestRepository matchRequestRepository)
    {
        _context = context;
        _matchRequestRepository = matchRequestRepository;
    }
    [HttpPost]
    public async Task<IActionResult> CreateMatchRequest([FromBody] CreateMatchRequestDto matchRequestDto)
    {
        try
        {
            if (matchRequestDto == null)
            {
                return BadRequest(new { message = "Dữ liệu ghép đối không phù hợp." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var request = await _matchRequestRepository.CreateMatchRequestAsync(matchRequestDto);
            if (request == null)
            {
                return BadRequest(new { message = "Không thể tạo yêu cầu ghép đối." });
            }
            return Ok(new
            {
                message = "Tạo Yêu cầu ghép đối hợp lệ",
                data = request,
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
    [HttpGet("{pitchId}")]
    public async Task<IActionResult> GetMatchRequestByPitch([FromRoute] int pitchId)
    {
        try
        {
            var matchRequest = await _matchRequestRepository.GetMatchRequestsAsync(pitchId);
            if (matchRequest == null)
            {
                return BadRequest(new
                {
                    message = "Không tìm thấy yêu cầu bắt đối nào.",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Tìm thấy yêu cầu bắt đối của sân.",
                data = matchRequest,
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
    [HttpGet]
    public async Task<IActionResult> GetAllMatchRequest()
    {
        try
        {
            var matchRequest = await _matchRequestRepository.GetAllMatchRequest();
            if (matchRequest == null)
            {
                return BadRequest(new
                {
                    message = "Không tìm thấy yêu cầu bắt đối nào.",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Tìm kiếm thông tin bắt đối thành công,",
                data = matchRequest,
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