using Backend.Dtos.Report;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class ReportRepository : IReportRepository

{
    private readonly BongDa24HContext _context;
    public ReportRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<CreateReportDto> CreateReportAsyn(CreateReportDto createReportDto)
    {
        _context.Reports.Add(createReportDto.ToCreateReport());
        await _context.SaveChangesAsync();
        return createReportDto;
    }

    public async Task<Report?> GetReportById(int Id)
    {
        return await _context.Reports.FirstOrDefaultAsync(r => r.Id == Id);
    }

    public async Task<Report> UpdateReportAsyn(Report report)
    {
        _context.Reports.Update(report);
        await _context.SaveChangesAsync();
        return report;
    }
}