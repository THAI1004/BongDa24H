using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos;

public class CreateTeamDto
{
    [Required(ErrorMessage = "Yêu cầu cung cấp Tên đội.")]

    public string TeamName { get; set; } = null!;
    [Required(ErrorMessage = "Yêu cầu cung cấp Người tạo đội.")]

    public int ManagerId { get; set; }

    public int? TotalMatches { get; set; }

    public int? Wins { get; set; }

    public int? SkillLevel { get; set; }
    public string? Image { get; set; }

}