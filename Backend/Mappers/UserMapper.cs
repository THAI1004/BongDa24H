using Backend.Dtos;
using Backend.Models;

namespace Backend.Mappers;

public static class UserMapper
{
    public static User ToUserDto(this UserDto userDto)
    {
        return new User
        {
            FullName = userDto.FullName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Role = userDto.Role,
            AccumulatedPoints = userDto.AccumulatedPoints
        };
    }
    public static User ToCreateUserDto(this CreateUserDto createUserDto)
    {
        return new User
        {
            FullName = createUserDto.FullName,
            Email = createUserDto.Email,
            PasswordHash = createUserDto.PasswordHash,
            PhoneNumber = createUserDto.PhoneNumber,
            Role = createUserDto.Role,
            AccumulatedPoints = createUserDto.AccumulatedPoints
        };
    }
}