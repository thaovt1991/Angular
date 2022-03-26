using MINI_PROJECT_APIs.Domain.Responses.Department;
using MINI_PROJECT_APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MINI_PROJECT_APIs.Service
{
    public interface IDepartmentService
    {

        Task<List<Department>> GetDepartments();
        Task<List<DepartmentRes>> GetDepartmentsTreeById(int id);
        Task<Department> GetDepartmentById(int id);
        Task<Department> Create(Department department);
        Task<Department> Modify(Department department);
        Task<Department> Remove(int id);
        Task <List<int>> getAllIdDepartmnet(List<DepartmentRes> departmentsRes, List<int> listId);
        Task<List<DepartmentRes>> DepartmentTreeBySearch(List<Department> childrenList);
        Task<List<Department>> SearchDepartmentByKeyword(string keyword);
    }
}
