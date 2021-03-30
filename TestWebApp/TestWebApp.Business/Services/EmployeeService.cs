using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestWebApp.Data.DTOs;
using TestWebApp.Data.Models;

namespace TestWebApp.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static HttpClient httpClient = new HttpClient();
        public async Task<IEnumerable<EmployeeModel>> GetActiveEmployeesAsync()
        {
            var getEmployeesTask = httpClient.GetAsync("https://guestservices-sreint-uw-fa-sb.azurewebsites.net/api/Employee");
            var getDepartmentsTask = httpClient.GetAsync("https://guestservices-sreint-uw-fa-sb.azurewebsites.net/api/Department");

            Task.WaitAll(new Task<HttpResponseMessage>[] { getEmployeesTask , getDepartmentsTask });

            var employeesString = await getEmployeesTask.Result.Content.ReadAsStringAsync();

            var departmentsString = await getDepartmentsTask.Result.Content.ReadAsStringAsync();

            var employees = JsonConvert.DeserializeObject<IEnumerable<Employee>>(employeesString);

            var departments = JsonConvert.DeserializeObject<IEnumerable<Department>>(departmentsString);

            //Time Complexity O(N*M) where N - num of emp, M - num of depts | Space Complexity O(1)
            // Hint1 - Use a hash table to get O(N+M)
            var activeEmployeeModels = employees.Where(_ => _.IsActive).Select(_ => {
                var model = new EmployeeModel()
                {
                    Id = _.Id,
                    Name = _.Name,
                    Age = _.Age,
                    DepartmentName = 
                        departments.FirstOrDefault(x => x.Id == _.DepartmentId)?.Name
                };                

                return model;
            });

            return activeEmployeeModels;
        }
    }
}
