using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos;

public class LoginDto
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;

}