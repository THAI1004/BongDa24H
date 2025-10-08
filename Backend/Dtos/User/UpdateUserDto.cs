using Microsoft.AspNetCore.Http;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public int? Role { get; set; }
    public int AccumulatedPoints { get; set; }
    public string? Image { get; set; } // đường dẫn ảnh (sau khi upload)
    public IFormFile? ImageFile { get; set; } // file ảnh upload mới
}
