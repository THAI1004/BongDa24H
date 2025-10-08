using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos.Pitch;

public class CreatePitchDto
{
    [Required(ErrorMessage = "Yêu cầu cung cấp tên sân.")]
    public string PitchName { get; set; } = null!;
    [Required(ErrorMessage = "Yêu cầu cung cấp ID cụm sân.")]

    public int ClusterId { get; set; }

    public string? ImageUrl { get; set; }

    public string? Facilities { get; set; }

    public int? PitchType { get; set; }

}