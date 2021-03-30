using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.Data.Models;

namespace TestWebApp.Business.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetActiveEmployeesAsync();
    } 
}
