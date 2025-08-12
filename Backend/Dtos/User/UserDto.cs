namespace Backend.Dtos;

public class UserDto
{
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? Role { get; set; }

    public int? AccumulatedPoints { get; set; }

}