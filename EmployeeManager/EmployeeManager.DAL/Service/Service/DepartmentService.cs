
using EmployeeManager.DAL.Models;
using EmployeeManager.DAL.Repository.IRepository;
using EmployeeManager.DAL.Service.IService;
using EmployeeManager.Domain.Responses.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Service.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }


        public async Task<List<Department>> GetDepartments()
        {
            return await departmentRepository.GetDepartments();
        }
        public async Task<Department> Create(Department department)
        {
            return await departmentRepository.Create(department);
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await departmentRepository.GetDepartmentById(id);
        }

        public async Task<Department> Modify(Department department)
        {
            return await departmentRepository.Modify(department);
        }

        public async Task<Department> Remove(int id)
        {
            return await departmentRepository.Remove(id);
        }


        public async Task<List<Department>> SearchDepartmentByKeyword(string keyword)
        {
            return await departmentRepository.SearchDepartmentByKeyword(keyword);
        }

        public async Task<List<DepartmentRes>> GetDepartmentsTreeById(int id)
        {
            List<Department> departments = await departmentRepository.GetDepartments();

            List<DepartmentRes> departmentsRes = new List<DepartmentRes>();
            foreach (Department d in departments)
            {
                departmentsRes.Add(toDepartmentRes(d));
            }
            List<DepartmentRes> hierarchy = new List<DepartmentRes>();

            hierarchy = departmentsRes
                   .Where(c => c.ParentId == id)
                   .Select(c => new DepartmentRes()
                   {
                       Id = c.Id,
                       Name = c.Name,
                       ParentId = c.ParentId,
                       Children = GetChildren(departmentsRes, c.Id)
                   })
                   .ToList();

            return hierarchy;
        }


        public async Task<List<int>> GetAllIdDepartmnet(List<DepartmentRes> departmentsRes, List<int> listId)
        {
            foreach (DepartmentRes d in departmentsRes)
            {
                if (d.Children != null)
                {
                    listId.Add(d.Id);
                    listId = await GetAllIdDepartmnet(d.Children, listId);
                }
            }
            return listId;
        }

        private List<DepartmentRes> GetChildren(List<DepartmentRes> departmentsRes, int parentId)
        {
            return departmentsRes
                    .Where(c => c.ParentId == parentId)
                    .Select(c => new DepartmentRes
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        Children = GetChildren(departmentsRes, c.Id)
                    })
                    .ToList();
        }

        public async Task<List<DepartmentRes>> DepartmentTreeBySearch(List<Department> childrenList)
        {

            List<Department> departmentsSearch = new List<Department>();
            List<Department> departments = new List<Department>();
            foreach (Department d in childrenList)
            {
                departments = await GetListDepartmentByChildren(d, departments);
               
                foreach (Department dp in departments)
                {
                    if (departmentsSearch.IndexOf(dp) == -1)
                    {
                        departmentsSearch.Add(dp);
                    }
                }
            } 

            List<DepartmentRes> departmentsRes = new List<DepartmentRes>();
            foreach (Department d in departmentsSearch)
            {
                departmentsRes.Add(toDepartmentRes(d));
            }
            List<DepartmentRes> hierarchy = new List<DepartmentRes>();

            hierarchy = departmentsRes
                   .Where(c => c.ParentId == 0)
                   .Select(c => new DepartmentRes()
                   {
                       Id = c.Id,
                       Name = c.Name,
                       ParentId = c.ParentId,
                       Children = GetChildren(departmentsRes, c.Id)
                   })
                   .ToList();

            return hierarchy;
        }
        private async Task<List<Department>> GetListDepartmentByChildren(Department department, List<Department> listDepartmnet)
        {
            listDepartmnet.Add(department);
            if (department.ParentId != 0)
            {
                department = await GetDepartmentById(department.ParentId);
                listDepartmnet = await GetListDepartmentByChildren(department, listDepartmnet);
            }
            return listDepartmnet;
        }

        private DepartmentRes toDepartmentRes(Department department)
        {
            DepartmentRes departmentRes = new DepartmentRes();
            departmentRes.Id = department.Id;
            departmentRes.Name = department.Name;
            departmentRes.ParentId = department.ParentId;
            return departmentRes;
        }


        

        private DepartmentView ToDepartment(Department department)
        {

            DepartmentView departmentView = new DepartmentView();
            departmentView.Id = department.Id;
            departmentView.Name = department.Name;
            departmentView.ParentId = department.ParentId;
            return departmentView;
        }

        public List<DepartmentView> ToDepartmentList(List<Department> departments )
        {
            List<DepartmentView> departmnetViews = new List<DepartmentView>();
            foreach (Department d in departments)
            {
                DepartmentView dv = ToDepartment(d);
                departmnetViews.Add(dv);
            }
            return departmnetViews;
        }
    }
}
