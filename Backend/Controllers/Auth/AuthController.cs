using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Backend.Service;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Admin;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly BongDa24HContext _context;
    private readonly JwtService _jwt;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration, BongDa24HContext context, JwtService jwt, IUserRepository userRepository)
    {
        _configuration = configuration;
        _context = context;
        _jwt = jwt;
        _userRepository = userRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        try
        {
            var response = await _userRepository.GetAllUserAsyn();
            if (response == null) return BadRequest("Không có tài khoản nào.");
            return Ok(new
            {
                data = response,
                message = "Lấy tài khoản thành công.",
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Lấy tài khoản thất bại",
                details = ex.InnerException?.Message ?? ex.Message
            });
        }
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
    {
        if (_context.Users.Any(u => u.Email == userDto.Email))
            return BadRequest("Email đã tồn tại.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash);
        userDto.PasswordHash = hashedPassword;
        await _userRepository.CreateUserAsyn(userDto);
        return Ok(new
        {
            message = "Tạo Tài khoản thành công.",
            success = true,
            data = userDto
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var isLogin = await _userRepository.CheckLogin(loginDto.Email);
        if (isLogin == null || !BCrypt.Net.BCrypt.Verify(loginDto.PasswordHash, isLogin.PasswordHash))
            return Unauthorized("Sai tài khoản hoặc mật khẩu.");

        var token = _jwt.GenerateToken(isLogin.Email);
        return Ok(new
        {
            message = "Đăng nhập thành công.",
            success = true,
            User = isLogin,
            Token = token
        });
    }
    [HttpPost("googleLogin")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        // Lấy Client ID và Client Secret từ cấu hình
        var googleClientId = _configuration["Google:ClientId"];
        var googleClientSecret = _configuration["Google:ClientSecret"]; // 👈 PHẢI CÓ Client Secret!

        if (string.IsNullOrEmpty(request.Code))
        {
            return BadRequest(new { message = "Authorization Code is missing.", success = false });
        }

        try
        {
            // 1. Cấu hình Luồng ủy quyền (Authorization Flow)
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = googleClientId,
                    ClientSecret = googleClientSecret // 👈 Dùng Client Secret ở đây
                },
                Scopes = new[] { "openid", "email", "profile" }
            });

            // 2. Trao đổi Authorization Code lấy Token Response (chứa ID Token)
            TokenResponse tokenResponse = await flow.ExchangeCodeForTokenAsync(
                "user", // Dùng ID tạm thời, không quan trọng
                request.Code, // Mã ủy quyền nhận từ Frontend
                request.RedirectUri, // Cần khớp với Redirect URI đã đăng ký (thường là "postmessage")
                CancellationToken.None); // 💡 Đảm bảo CancellationToken.None là đối số thứ 4 và cuối cùng.

            // 3. Lấy và Xác thực ID Token (JWT)
            // ID Token nằm trong TokenResponse vừa nhận được
            string idToken = tokenResponse.IdToken;

            if (string.IsNullOrEmpty(idToken))
            {
                // Rất hiếm khi xảy ra, nhưng cần kiểm tra
                return StatusCode(500, new { message = "Failed to retrieve ID Token from Google.", success = false });
            }

            // 4. Xác thực ID Token JWT (Dùng chính logic của bạn, nhưng với ID Token thật)
            var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { googleClientId }
            };

            // ValidateAsync sẽ thành công vì đây là ID Token hợp lệ
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);

            // 5. Xử lý logic nghiệp vụ (Kiểm tra và tạo user)
            var user = await _userRepository.CheckLogin(payload.Email);

            if (user == null)
            {
                var newUser = new CreateUserDto
                {
                    Email = payload.Email,
                    FullName = payload.Name,
                    PasswordHash = Guid.NewGuid().ToString(), // fake password cho user Google,
                    Role = 0
                };
                await _userRepository.CreateUserAsyn(newUser);
            }
            user = await _userRepository.CheckLogin(payload.Email); // Lấy lại thông tin user vừa tạo

            // 6. Tạo JWT token nội bộ
            var token = _jwt.GenerateToken(payload.Email);

            // Trả về kết quả
            return Ok(new { token = token, message = "Login successful via Google.", success = true, user = user });
        }
        catch (InvalidJwtException jwtEx)
        {
            // Bắt lỗi cụ thể liên quan đến JWT/Token không hợp lệ
            // Lỗi này xảy ra nếu ID Token không hợp lệ hoặc đã hết hạn
            return Unauthorized(new { message = "Google token validation failed.", details = jwtEx.Message });
        }
        catch (Exception ex)
        {
            // Lỗi chung (ví dụ: Google trả về lỗi khi trao đổi Code - 400 Bad Request)
            // Đây là nơi bạn nên kiểm tra log để xem Google trả về lỗi gì.
            return StatusCode(500, new { message = "Google login process failed.", details = ex.Message });
        }
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto userDto, [FromRoute] int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found");

        // Cập nhật thông tin cơ bản
        user.FullName = userDto.FullName ?? user.FullName;
        user.PhoneNumber = userDto.PhoneNumber ?? user.PhoneNumber;

        // 🖼️ Nếu có upload ảnh mới
        if (userDto.ImageFile != null && userDto.ImageFile.Length > 0)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // 🔥 Xóa ảnh cũ nếu có
            if (!string.IsNullOrEmpty(user.Image))
            {
                var relativePath = user.Image.TrimStart('/'); // "/uploads/abc.jpg" -> "uploads/abc.jpg"
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));



                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);

                }

            }

            // 📸 Lưu ảnh mới
            var fileName = $"{Guid.NewGuid()}_{userDto.ImageFile.FileName}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await userDto.ImageFile.CopyToAsync(stream);
            }

            user.Image = $"/uploads/{fileName}";
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật thành công!", data = user });
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy người dùng cần cập nhật." });
            }
            return Ok(new
            {
                data = user,
                message = "lấy người dùng thành công.",
                success = true
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Lấy tài khoản thất bại",
                details = ex.InnerException?.Message ?? ex.Message
            });
        }
    }

}