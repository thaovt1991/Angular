
using EmployeeManager.DAL.Data;
using EmployeeManager.DAL.Models;
using EmployeeManager.DAL.Repository.IRepository;
using EmployeeManager.Domain.Responses.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Repository.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly AppDbContext context;

        public DepartmentRepository(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<List<Department>> GetDepartments()
        {
            return await context.Departments.Include(d => d.Employees).Where(d => d.Deleted == false).ToListAsync();
        }
        public async Task<Department> Create(Department department)
        {
            try
            {
                context.Departments.Add(department);
               await context.SaveChangesAsync();
           
                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await context.Departments.Include(d => d.Employees).Where(d => !d.Deleted && d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Department> Modify(Department department)
        {
            try
            {
                context.Attach(department);
                context.Entry<Department>(department).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Department> Remove(int id)
        {
            try
            {
                var department = await GetDepartmentById(id);
                department.Deleted = true;
                context.Attach(department);
                context.Entry<Department>(department).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<Department>> SearchDepartmentByKeyword(string keyword)
        {
            return await context.Departments.Where(d => d.Name.Contains(keyword)).ToListAsync();
        }

    }
}

