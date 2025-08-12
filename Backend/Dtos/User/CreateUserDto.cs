namespace Backend.Dtos;

public class CreateUserDto
{
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? Role { get; set; }

    public int? AccumulatedPoints { get; set; }

}