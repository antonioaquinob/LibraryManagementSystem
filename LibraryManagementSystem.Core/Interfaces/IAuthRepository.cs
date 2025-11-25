using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.Entities;
namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task SaveChangesAsync();

    }
}
