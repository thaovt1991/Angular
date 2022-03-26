﻿
using EmployeeManager.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.DAL.Data;
using EmployeeManager.DAL.Models;

namespace EmployeeManager.DAL.Repository.Repository
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        private readonly AppDbContext context;


        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;

        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await context.Employees.Include(e => e.Department).Where(e => !e.Deleted).ToListAsync();
        }
        public async Task<Employee> Create(Employee employee)
        {
            try
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync();
                

                return employee;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await context.Employees.Include(e => e.Department).Where(e => !e.Deleted && e.Id == id).FirstOrDefaultAsync();

        }

        public async Task<Employee> Modify(Employee employee)
        {
            try
            {
                context.Attach(employee);
                context.Entry<Employee>(employee).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> Remove(int id)
        {
            try
            {
                var employee = await GetEmployeeById(id);
                employee.Deleted = true;
                context.Attach(employee);
                context.Entry<Employee>(employee).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EmployeeExists(int id)
        {
            return context.Employees.Any(e => e.Id == id);
        }

        public async Task<List<Employee>> GetEmployeesByDerpartmentId(int id)
        {
            return await context.Employees.Include(e => e.Department).Where(e => !e.Deleted && e.Department.Id == id).ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesSearchBy(string field, string oper, string value)
        {
            List<Employee> employeesSearch = new List<Employee>();

            string operatorSQL = "";

            switch (oper)
            {
                case "contains":
                    operatorSQL = "like N'%" + value + "%'";
                    break;
                case "eq":
                    operatorSQL = "= N'" + value + "'";
                    break;
                case "neq":
                    operatorSQL = "<> N'" + value + "'";
                    break;
                case "doesnotcontain":
                    operatorSQL = "not like N'%" + value + "%'";
                    break;
                case "startswith":
                    operatorSQL = "% STARTSWITH  N'" + value + "'";
                    break;
                case "endswith":
                    operatorSQL = "like N'%" + value + "'";
                    break;
                case "isnull":
                    operatorSQL = "IS NULL";
                    break;
                case "isnotnull":
                    operatorSQL = "IS NOT NULL";
                    break;
                case "isempty":
                    operatorSQL = "IS EMPTY";
                    break;
                case "isnotempty":
                    operatorSQL = "IS NOT EMPTY";
                    break;
            }
            string query = "";
            if (field != "departmentName")
            {
                 query = "select * from  Employees Where " + field + " " + operatorSQL;
            }
            else
            {
                query = "SELECT * FROM Employees as e JOIN (select  id ,name as DepartmentName from Departments  where Name " + operatorSQL +") as d ON e.DepartmentId = d.Id ";
            }


            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            //var number2 = adapter.Fill(ds, "tbResult");
            //dt = ds.Tables["tbResult"];
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var idDepartment = int.Parse(row["DepartmentId"].ToString());
                Department department = await context.Departments.Include(d => d.Employees).Where(d => !d.Deleted && d.Id == idDepartment).FirstOrDefaultAsync(); ;
                if (department != null)
                {
                    Employee emp = new Employee()
                    {
                        Id = int.Parse(row["Id"].ToString()),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Position = row["Position"].ToString(),
                        Title = row["Title"].ToString(),
                        AvatarPath = row["AvatarPath"].ToString(),
                        Department = department,
                    };
                    employeesSearch.Add(emp);
                }
            }
            return employeesSearch;
        }

    }
}

