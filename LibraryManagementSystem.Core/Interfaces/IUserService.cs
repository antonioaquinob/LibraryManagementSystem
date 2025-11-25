using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> RegisterUserAsync(RegisterUserDto dto);
        Task<User?> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
        Task<string?> AuthenticateUserAsync(LoginDto dto); // returns JWT token
    }
}
