using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DomainModel.Models;

namespace Eshop.DataAccess.Services
{
    public interface IUserServiceContract
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<bool> ExistsUserAsync(string? userName, string? password);
        Task<User?> GetAsync(string? userName, string? password);
    }
}
