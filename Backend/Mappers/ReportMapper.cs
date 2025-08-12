using Backend.Dtos.Report;
using Backend.Models;

namespace Backend.Mappers;

public static class ReportMapper
{
    public static Report ToCreateReport(this CreateReportDto createReportDto)
    {
        return new Report
        {
            ReporterId = createReportDto.ReporterId,
            TargetId = createReportDto.TargetId,
            TargetType = createReportDto.TargetType,
            Reason = createReportDto.Reason,
            ReportDate = createReportDto.ReportDate,
            Status = createReportDto.Status
        };
    }
}