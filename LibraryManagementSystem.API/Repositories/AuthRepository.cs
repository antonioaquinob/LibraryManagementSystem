using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.API.Data;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementSystem.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LibraryDbContext _context;
        public AuthRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
