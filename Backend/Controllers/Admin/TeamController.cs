using Backend.Dtos;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[Route("api/team")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeamRepository _teamRepository;
    public TeamController(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllTeam()
    {
        try
        {
            var teams = await _teamRepository.GetAllTeamAsyn();
            if (teams == null)
            {
                return BadRequest(new
                {
                    message = "Không có team nào.",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Lấy danh sách đội thành công.",
                data = teams,
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
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTeam(CreateTeamDto createTeamDto)


    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var team = await _teamRepository.CreateTeamAsyn(createTeamDto);
            if (team == null)
            {
                return BadRequest(new
                {
                    message = "Tạo team không thành công.",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Tạo team thành công.",
                data = team,
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
    [Authorize]
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateTeam([FromBody] CreateTeamDto createTeamDto, [FromRoute] int Id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var team = await _teamRepository.GetTeamByIdAsyn(Id);
            if (team == null)
            {
                return BadRequest(new
                {
                    message = "Không tìm thấy team cần sửa.",
                    success = false
                });
            }
            team.TeamName = createTeamDto.TeamName;
            team.TotalMatches = createTeamDto.TotalMatches;
            team.Wins = createTeamDto.Wins;
            team.SkillLevel = createTeamDto.SkillLevel;
            var teamnew = await _teamRepository.UpdateTeamAsyn(team);
            if (teamnew == null)
            {
                return BadRequest(new
                {
                    message = "Cập nhật team thất bại.",
                    success = false
                });
            }
            return Ok(new
            {
                message = "Cập nhật đội thành công.",
                data = teamnew,
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
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam([FromRoute] int id)
    {
        try
        {
            var team = await _teamRepository.DeleteTeam(id);
            if (team == null)
            {
                return NotFound("Không tìm thấy team.");
            }
            return Ok(new
            {
                data = team,
                message = "Xóa đội thành công.",
                status = true
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
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamById([FromRoute] int id)
    {
        try
        {
            var response = await _teamRepository.GetTeamByIdAsyn(id);
            if (response == null)
            {
                return NotFound("Không tìm thấy đội.");
            }
            return Ok(new
            {
                data = response,
                message = "Lấy chi tiết đội thành công.",
                status = true
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