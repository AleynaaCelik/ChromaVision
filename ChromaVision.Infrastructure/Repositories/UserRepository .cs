using ChromaVision.Application.Common.Interfaces;
using ChromaVision.Domain.Entities;
using ChromaVision.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        private new readonly IApplicationDbContext _dbContext;

        public UserRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // UserRepository.cs
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower()) ?? new User("", "", ""); // Null safety
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
