
using EmployeeManager.DAL.Models;
using EmployeeManager.Domain.Responses.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Service.IService
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetDepartments();
        Task<List<DepartmentRes>> GetDepartmentsTreeById(int id);
        Task<Department> GetDepartmentById(int id);
        Task<Department> Create(Department department);
        Task<Department> Modify(Department department);
        Task<Department> Remove(int id);
        Task<List<int>> GetAllIdDepartmnet(List<DepartmentRes> departmentsRes, List<int> listId);
        Task<List<DepartmentRes>> DepartmentTreeBySearch(List<Department> childrenList);
        Task<List<Department>> SearchDepartmentByKeyword(string keyword);

        List<DepartmentView> ToDepartmentList(List<Department> departments);
    }
}
