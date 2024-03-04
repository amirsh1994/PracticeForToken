using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DomainModel.Models;

namespace Eshop.DataAccess.Services
{
    public interface IEmployeeServiceContract
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetAsync(int employeeId);
    }
}
