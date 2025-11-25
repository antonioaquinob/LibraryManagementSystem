using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using System.Threading.Tasks;
using BCrypt.Net;

namespace LibraryManagementSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
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

        public async Task<User?> AuthenticateUserAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.Username);
            if (user == null)
                return null;

            bool isPasswordMatches = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            return isPasswordMatches ? user : null;
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
