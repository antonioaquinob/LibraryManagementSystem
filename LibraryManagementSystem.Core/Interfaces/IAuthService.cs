using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterUserDto dto);
        Task<string?> LoginAsync(LoginDto dto);
    }
}
