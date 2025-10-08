using Backend.Dtos.Report;
using Backend.Models;

namespace Backend.Interfaces;

public interface IReportRepository
{
    Task<CreateReportDto> CreateReportAsyn(CreateReportDto createReportDto);
    Task<Report> UpdateReportAsyn(Report report);
    Task<Report?> GetReportById(int Id);
}