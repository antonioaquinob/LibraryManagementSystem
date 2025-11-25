using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace LibraryManagementSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepo.GetUserById(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userRepo.GetByUsernameAsync(username);
        }

        public async Task<User> RegisterUserAsync(RegisterUserDto dto)
        {
            // Hash the password
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashPassword
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return user;
        }

        public async Task<string?> AuthenticateUserAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.Username);
            if (user == null) return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid) return null;

            // Create JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User?> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepo.GetUserById(id);
            if (user == null)
                return null;

            user.Username = dto.Username;
            user.Email= dto.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetUserById(id);
            if (user == null)
                return false;

            await _userRepo.DeleteAsync(user);
            await _userRepo.SaveChangesAsync();

            return true;
        }
    }
}
