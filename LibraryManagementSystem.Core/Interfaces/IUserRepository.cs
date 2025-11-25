using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
    }

}
