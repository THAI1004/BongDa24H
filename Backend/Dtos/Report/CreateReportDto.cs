namespace Backend.Dtos.Report;

public class CreateReportDto
{
    public int ReporterId { get; set; }

    public int TargetId { get; set; }

    public string TargetType { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public DateTime ReportDate { get; set; }

    public int? Status { get; set; }

}