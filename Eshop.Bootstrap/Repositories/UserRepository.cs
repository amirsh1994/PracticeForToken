using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DataAccess.Services;
using Eshop.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.DataAccess.Repositories
{
    public class UserRepository(NorthWindtahlildadehContext db) : IUserServiceContract
    {
        private readonly NorthWindtahlildadehContext _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<bool> ExistsUserAsync(string? userName, string? password)
        {
            return await _db.Users.AnyAsync(x => x.UserName == userName && x.Password == password);
        }

        public async Task<User?> GetAsync(string? userName, string? password)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
        }
    }
}
