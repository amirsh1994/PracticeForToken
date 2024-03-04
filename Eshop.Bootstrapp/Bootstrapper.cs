using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DataAccess.Repositories;
using Eshop.DataAccess.Services;
using Eshop.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Bootstrapp
{
    public static class Bootstrapper
    {
        public static void WiredUp(this IServiceCollection service, string cnnString)
        {
            service.AddDbContext<NorthWindtahlildadehContext>(option =>
            {
                option.UseSqlServer(cnnString);
            }, ServiceLifetime.Scoped);

            service.AddScoped<IUserServiceContract,UserRepository>();
            service.AddScoped<IEmployeeServiceContract,EmployeeRepository>();
        }
    }
}
