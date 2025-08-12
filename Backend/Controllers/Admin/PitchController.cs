using Backend.Dtos.Pitch;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Backend.Controllers.Admin;

[Route("api/pitch")]
[ApiController]
public class PitchController : ControllerBase
{
    private readonly BongDa24HContext _context;
    private readonly IPitchRepository _pitchRepository;
    public PitchController(BongDa24HContext context, IPitchRepository pitchRepository)
    {
        _pitchRepository = pitchRepository;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPitches()
    {
        var pitches = await _pitchRepository.GetAllPitchesAsync();
        if (pitches == null || !pitches.Any())
        {
            return NotFound(new
            {
                message = "Không tìm thấy sân nào",
                success = false
            });
        }
        return Ok(new
        {
            message = "Lấy danh sách sân thành công",
            data = pitches,
            success = true
        });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPitchById(int id)
    {
        try
        {
            var pitch = await _pitchRepository.GetPitchByIdAsync(id);
            return Ok(new
            {
                message = "Lấy sân thành công",
                data = pitch,
                success = true
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message,
                success = false
            });
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreatePitch([FromBody] CreatePitchDto createPitchDto)
    {
        try
        {
            if (createPitchDto == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu không hợp lệ",
                    success = false
                });
            }

            var createdPitch = await _pitchRepository.CreatePitchAsync(createPitchDto);
            return Ok(new
            {
                message = "Tạo sân thành công",
                data = createdPitch.ToPitchCreate(),
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
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePitch([FromRoute] int id, [FromBody] CreatePitchDto updatePitchDto)
    {
        try
        {
            var pitch = await _pitchRepository.GetPitchByIdAsync(id);
            if (pitch == null)
            {
                return NotFound(new
                {
                    message = "Sân không tồn tại",
                    success = false
                });
            }
            pitch.PitchName = updatePitchDto.PitchName;
            pitch.ClusterId = updatePitchDto.ClusterId;
            pitch.PitchType = updatePitchDto.PitchType;
            pitch.ImageUrl = updatePitchDto.ImageUrl;
            pitch.Facilities = updatePitchDto.Facilities;
            await _pitchRepository.UpdatePitchAsync(pitch);
            return Ok(new
            {
                message = "Cập nhật sân thành công.",
                data = updatePitchDto.ToPitchCreate(),
                success = true
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message,
                success = false
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePitch([FromRoute] int id)
    {
        try
        {
            var isDeleted = await _pitchRepository.DeletePitchAsync(id);
            if (!isDeleted)
            {
                return NotFound(new
                {
                    message = "Sân không tồn tại",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Xoá sân thành công",
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