using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.MatchResponse;

public class MatchResponseDto
{
    [Required(ErrorMessage = "Id Yêu cầu bắt đối không được để trống.")]
    public int RequestId { get; set; }
    [Required(ErrorMessage = "Id người nhận không được để trống.")]


    public int ResponderId { get; set; }

    public string? Content { get; set; }

    public int? Status { get; set; }

}