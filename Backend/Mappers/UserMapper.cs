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
            AccumulatedPoints = userDto.AccumulatedPoints,
            Image = userDto.Image
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
            ,
            Image = createUserDto.Image

        };
    }
    public static User ToUpdateUserDto(this UpdateUserDto updateUserDto)
    {
        return new User
        {
            FullName = updateUserDto.FullName,
            Email = updateUserDto.Email,
            PasswordHash = updateUserDto.PasswordHash,
            PhoneNumber = updateUserDto.PhoneNumber,
            Role = updateUserDto.Role,
            AccumulatedPoints = updateUserDto.AccumulatedPoints
            ,
            Image = updateUserDto.Image

        };
    }
    public static User ToLoginDto(LoginDto loginDto)
    {
        return new User
        {
            Email = loginDto.Email,
            PasswordHash = loginDto.PasswordHash
        };
    }

}