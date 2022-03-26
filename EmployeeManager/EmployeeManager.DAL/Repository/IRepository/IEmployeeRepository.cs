using EmployeeManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<List<Employee>> GetEmployeesSearchBy(string field, string oper, string value);
        Task<List<Employee>> GetEmployeesByDerpartmentId(int id);
        Task<Employee> Create(Employee employee);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> Modify(Employee employee);
        Task<Employee> Remove(int id);
        bool EmployeeExists(int id);


    }
}
