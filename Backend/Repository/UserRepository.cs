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
    public async Task<CreateUserDto> CreateUserAsun(CreateUserDto createUserDto)
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
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
    }

    public async Task<User> UpdateUserAsyn(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}