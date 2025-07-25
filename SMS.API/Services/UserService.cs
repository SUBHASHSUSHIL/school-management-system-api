using Microsoft.EntityFrameworkCore;
using SMS.API.Data;
using SMS.API.Services.Interfaces;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var newUser = new User
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth
            };
            await _applicationDbContext.Users.AddAsync(newUser);
            await _applicationDbContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            _applicationDbContext.Users.Remove(user);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _applicationDbContext.Users.AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return user;
        }

        public async Task<User> UpdateUserAsync(int userId, User user)
        {
            var existingUser = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            existingUser.Username = user.Username;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Email = user.Email;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Phone = user.Phone;
            existingUser.Address = user.Address;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.CreatedAt = user.CreatedAt;
            _applicationDbContext.Users.Update(existingUser);
            var result = await _applicationDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return existingUser;
            }
            else
            {
                throw new Exception("Failed to update user.");
            }
        }
    }
}
