using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUserAsyn();
    Task<User?> GetUserById(int Id);
    Task<CreateUserDto> CreateUserAsun(CreateUserDto createUserDto);
    Task<User> UpdateUserAsyn(User user);
}