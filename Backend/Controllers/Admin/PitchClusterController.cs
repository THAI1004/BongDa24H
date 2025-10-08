using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Mappers;
using Backend.Dtos.PitchCluster;
using Backend.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers.Admin;

[Route("api/pitchcluster")]
[ApiController]
public class PitchClusterController : ControllerBase
{
    private readonly BongDa24HContext _context;
    private readonly IPitchClusterRepository _pitchClusterRepository;
    public PitchClusterController(BongDa24HContext context, IPitchClusterRepository pitchClusterRepository)
    {
        _pitchClusterRepository = pitchClusterRepository;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pitch = await _pitchClusterRepository.GetAllPitchCluster();
        if (pitch == null || !pitch.Any())
        {
            return NotFound(new
            {
                message = "Lấy danh sách cụm sân không thành công",
                success = false
            });
        }
        return Ok(new
        {
            data = pitch,
            message = "Lấy danh sách cụm sân thành công",
            success = true
        });
    }
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var pitchCluster = _pitchClusterRepository.GetPitchClusterById(id).Result;
        if (pitchCluster == null)
        {
            return NotFound(new
            {
                message = "Cụm sân không tồn tại",
                success = false
            });
        }
        return Ok(new
        {
            data = pitchCluster,
            message = "Lấy thông tin cụm sân thành công. ",
            success = true
        });
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePitchClusterDto pitchClusterDto)
    {
        var pitchCluster = pitchClusterDto.ToPitchCluster();
        await _pitchClusterRepository.Create(pitchCluster);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (pitchCluster == null)
        {
            return BadRequest(new
            {
                message = "Tạo cụm sân không thành công",
                success = false
            });
        }
        return Ok(new
        {
            data = pitchCluster,
            message = "Tạo cụm sân thành công",
            success = true
        });
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreatePitchClusterDto pitchClusterDto)
    {
        var pitchCluster = _context.PitchClusters.FirstOrDefault(p => p.Id == id);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (pitchCluster == null)
        {
            return NotFound(new
            {
                message = "Cụm sân không tồn tại.",
                success = false
            });
        }
        pitchCluster.ClusterName = pitchClusterDto.ClusterName;
        pitchCluster.Address = pitchClusterDto.Address;
        pitchCluster.Longitude = pitchClusterDto.Longitude;
        pitchCluster.Latitude = pitchClusterDto.Latitude;
        pitchCluster.OwnerId = pitchClusterDto.OwnerId;
        await _pitchClusterRepository.Update(pitchCluster);
        return Ok(new
        {
            data = pitchCluster,
            message = "Cập nhật cụm sân thành công",
            success = true
        });
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var pitchCluster = await _pitchClusterRepository.GetPitchClusterById(id);
        if (pitchCluster == null)
        {
            return NotFound(new
            {
                message = "Không tìm thấy cụm sân.",
                success = false
            });
        }
        await _pitchClusterRepository.Delete(pitchCluster);
        return Ok(new
        {
            message = "Xoá cụm sân thành công",
            success = true
        });
    }

}