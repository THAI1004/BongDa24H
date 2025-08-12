using Backend.Dtos.Report;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[Route("api/report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportRepository _reportRepository;
    public ReportController(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    [HttpPost]
    public async Task<IActionResult> CreateReport(CreateReportDto createReportDto)
    {
        try
        {
            if (createReportDto == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu không hợp lệ.",
                    success = false
                });
            }
            await _reportRepository.CreateReportAsyn(createReportDto);
            return Ok(new
            {
                message = "Tạo báo cáo thành công.",
                data = createReportDto,
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
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateReport([FromRoute] int Id, [FromBody] CreateReportDto createReportDto)
    {
        try
        {
            var report = await _reportRepository.GetReportById(Id);
            if (report == null)
            {
                return BadRequest(new
                {
                    message = "Không tìm thấy báo cáo.",
                    success = false
                });
            }
            if (createReportDto == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu báo cáo không hợp lệ.",
                    success = false
                });
            }
            report.Status = createReportDto.Status;
            await _reportRepository.UpdateReportAsyn(report);
            return Ok(new
            {
                message = "Cập nhật báo cáo thành công.",
                data = report,
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

}