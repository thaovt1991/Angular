
using EmployeeManager.DAL.Models;
using EmployeeManager.DAL.Repository.IRepository;
using EmployeeManager.DAL.Service.IService;
using EmployeeManager.Domain.Responses.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;


        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;

        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await employeeRepository.GetEmployees();
        }
        public async Task<Employee> Create(Employee employee)
        {
            return await employeeRepository.Create(employee);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await employeeRepository.GetEmployeeById(id);

        }

        public async Task<Employee> Modify(Employee employee)
        {
            return await employeeRepository.Modify(employee);
        }

        public async Task<Employee> Remove(int id)
        {
            return await employeeRepository.Remove(id);
        }

        public bool EmployeeExists(int id)
        {
            return employeeRepository.EmployeeExists(id);
        }

        public async Task<List<Employee>> GetEmployeesByDerpartmentId(int id)
        {
            return await employeeRepository.GetEmployeesByDerpartmentId(id);
        }

        public async Task<List<Employee>> GetEmployeesSearchBy(string field, string oper, string value)
        {
            return await employeeRepository.GetEmployeesSearchBy(field, oper, value);
        }

        public List<EmployeeView> ToEmployeeList(List<Employee> employees)
        {
            List<EmployeeView> employeeViews = new List<EmployeeView>();
            foreach (Employee e in employees)
            {
                EmployeeView ev = ToEmployee(e);
                employeeViews.Add(ev);
            }
            return employeeViews;
        }

        public EmployeeView ToEmployee(Employee employee)
        {
            EmployeeView employeeView = new EmployeeView();
            employeeView.EmployeeId = employee.Id;
            employeeView.FirstName = employee.FirstName;
            employeeView.LastName = employee.LastName;
            employeeView.Deleted = employee.Deleted;
            employeeView.Position = employee.Position;
            employeeView.Title = employee.Title;
            employeeView.DepartmentId = employee.Department.Id;
            employeeView.DepartmentName = employee.Department.Name;
            //employeeView.AvatarPath = employee.AvatarPath;

            return employeeView;
        }
    }
}
