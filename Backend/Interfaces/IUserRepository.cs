using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUserAsyn();
    Task<User?> GetUserById(int Id);
    Task<CreateUserDto> CreateUserAsyn(CreateUserDto createUserDto);
    Task<User> UpdateUserAsyn(UpdateUserDto updateUserDto);
    Task<User?> CheckLogin(string email);

}