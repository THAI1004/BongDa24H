using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos;

public class CreateUserDto
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Yêu cầu cung cấp tên.")]

    public string FullName { get; set; } = null!;
    [Required(ErrorMessage = "Yêu cầu cung cấp mail.")]
    [EmailAddress(ErrorMessage = "Yêu cầu nhập mail")]
    public string Email { get; set; } = null!;

    [StringLength(6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự")]
    public string? PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? Role { get; set; }

    public int? AccumulatedPoints { get; set; }
    public string? Image { get; set; }

}