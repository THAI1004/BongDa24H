using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Report;

public class CreateReportDto
{
    [Required(ErrorMessage = "Yêu cầu cung cấp id người báo cáo.")]

    public int ReporterId { get; set; }

    public int TargetId { get; set; }

    public string TargetType { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public DateTime ReportDate { get; set; }

    public int? Status { get; set; }

}