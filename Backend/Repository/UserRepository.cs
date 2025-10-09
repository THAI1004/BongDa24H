using Backend.Dtos;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class UserRepository : IUserRepository
{
    private readonly BongDa24HContext _context;
    public UserRepository(BongDa24HContext context)
    {
        _context = context;
    }
    public async Task<CreateUserDto> CreateUserAsyn(CreateUserDto createUserDto)
    {
        _context.Users.Add(createUserDto.ToCreateUserDto());
        await _context.SaveChangesAsync();
        return createUserDto;
    }

    public async Task<List<User>> GetAllUserAsyn()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(int Id)
    {
        return await _context.Users.Include(t => t.Teams).Include(t => t.MatchRequests).Include(t => t.MatchResponses).Include(t => t.Reports).Include(t => t.Bookings).Include(t => t.PitchClusters)
        .FirstOrDefaultAsync(u => u.Id == Id);

    }


    public async Task<User> UpdateUserAsyn(UpdateUserDto updatedU)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedU.Id);
        if (existingUser == null)
            throw new Exception("User not found");

        // Cập nhật các trường cần thiết
        existingUser.FullName = updatedU.FullName;
        existingUser.Email = updatedU.Email;
        existingUser.PhoneNumber = updatedU.PhoneNumber;
        existingUser.Role = updatedU.Role;
        existingUser.AccumulatedPoints = updatedU.AccumulatedPoints;
        existingUser.Image = updatedU.Image;

        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<User?> CheckLogin(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}