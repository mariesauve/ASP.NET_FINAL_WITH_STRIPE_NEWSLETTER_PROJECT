
using System;
using System.Linq;
using System.Threading.Tasks;
using KoolStuff.Data;
using KoolStuff.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KoolStuff.Repositories
{
    public class EmailStoreRepository : IEmailStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public EmailStoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddEmail(string userName, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    throw new ArgumentException("User name cannot be null or empty", nameof(userName));

                if (string.IsNullOrEmpty(email))
                    throw new ArgumentException("Email cannot be null or empty", nameof(email));

                var existingEmail = await GetByEmail(email);
                if (existingEmail != null)
                {
                    // Email already exists, handle accordingly (e.g., return an error code)
                    return -1; // or any suitable error indicator
                }

                var newEmail = new EmailStore
                {
                    UserName = userName,
                    Email = email
                };

                _db.EmailStore.Add(newEmail);
                await _db.SaveChangesAsync();

                return 1; // or any suitable success indicator
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log or rethrow if necessary
                return -1; // or any suitable error indicator
            }
        }

        public Task<IActionResult> Details(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetByEmail(string email)
        {
            var emailStore = await _db.EmailStore.FirstOrDefaultAsync(x => x.Email == email);
            return emailStore?.UserName;
        }

        public Task<IActionResult> SaveEmail(string userName, string email)
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> IEmailStoreRepository.Index()
        {
            throw new NotImplementedException();
        }
    }
}