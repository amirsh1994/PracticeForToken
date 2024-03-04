using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Eshop.DataAccess.Services;
using Eshop.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.DataAccess.Repositories
{
    public class EmployeeRepository(NorthWindtahlildadehContext db) : IEmployeeServiceContract
    {
        private readonly NorthWindtahlildadehContext _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            //return await _db.Employees.OrderByDescending(x => x.EmployeeId).Take(5).ToListAsync();
            return await _db.Employees.Select(x=>new Employee {EmployeeId=x.EmployeeId, FirstName=x.FirstName,Title=x.Title}).ToListAsync();
        }

        public async Task<Employee?> GetAsync(int employeeId)
        {
            return await _db.Employees.FirstOrDefaultAsync(x=>x.EmployeeId==employeeId);
        }
    }
}
