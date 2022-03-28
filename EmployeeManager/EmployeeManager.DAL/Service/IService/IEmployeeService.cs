
using EmployeeManager.DAL.Models;
using EmployeeManager.Domain.Responses.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Service.IService
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
        Task<List<Employee>> GetEmployeesSearchBy(string field, string oper, string value);
        Task<List<Employee>> GetEmployeesByDerpartmentId(int id);
        Task<Employee> Create(Employee employee);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> Modify(Employee employee);
        Task<Employee> Remove(int id);
        bool EmployeeExists(int id);
        List<EmployeeView> ToEmployeeList(List<Employee> employees);

        public EmployeeView ToEmployee(Employee employee);
    }
}
