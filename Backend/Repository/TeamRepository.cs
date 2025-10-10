using Backend.Dtos;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class TeamRepository : ITeamRepository
{
    private readonly BongDa24HContext _context;
    public TeamRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<CreateTeamDto> CreateTeamAsyn(CreateTeamDto createTeamDto)
    {
        _context.Teams.Add(createTeamDto.ToCreateTeamDto());
        await _context.SaveChangesAsync();
        return createTeamDto;
    }
    public async Task<List<Team>> GetAllTeamAsyn()
    {
        return await _context.Teams.Include(t => t.Manager).ToListAsync();

    }

    public async Task<Team?> GetTeamByIdAsyn(int Id)
    {
        return await _context.Teams.Include(t => t.Manager)
        .FirstOrDefaultAsync(t => t.Id == Id);
    }

    public async Task<Team> UpdateTeamAsyn(Team team)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
        return team;
    }
    public async Task<Team?> DeleteTeam(int id)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team == null)
        {
            return null;
        }
        team.IsDeleted = true;
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
        return team;
    }
}