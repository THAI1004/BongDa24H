using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

public class PitchController : ControllerBase
{
    private readonly BongDa24HContext _context;
    public PitchController(BongDa24HContext context)
    {
        _context = context;
    }

}