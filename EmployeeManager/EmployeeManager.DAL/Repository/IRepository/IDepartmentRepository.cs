using EmployeeManager.DAL.Models;
using EmployeeManager.Domain.Responses.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> Create(Department department);
        Task<Department> Modify(Department department);
        Task<Department> Remove(int id);
        Task<List<Department>> SearchDepartmentByKeyword(string keyword);
    }
}
