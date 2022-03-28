
using EmployeeManager.Domain.Responses.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using EmployeeManager.DAL.Service.IService;
using EmployeeManager.DAL.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.Domain.Request;
using EmployeeManager.Domain.Responses.Department;
using EmployeeManager.DAL.Service.IService;
using EmployeeManager.Domain.Responses.Other;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;


        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> GetAllEmployees()
        {
            List<Employee> employees = await employeeService.GetEmployees();
            //employees = employees.OrderByDescending(e => e.Id).ToList();
            return employeeService.ToEmployeeList(employees);
            //return await employeeService.GetEmployees();
        }

        [HttpGet]
        [Route("/api/EmployeeByDepartmentId/{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> GetEmployeesByDepartmnetId(int id)
        {
            List<Employee> employees = await employeeService.GetEmployeesByDerpartmentId(id);
            return employeeService.ToEmployeeList(employees);
            //return await employeeService.GetEmployeesByDerpartmentId(id);
        }

        [HttpGet]
        [Route("/api/EmployeeOfTreeByDepartmentId/{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> GetEmployeesOfTreeByDepartmnetId(int id)
        {
            List<DepartmentRes> departmentRes = await departmentService.GetDepartmentsTreeById(id);
            List<Employee> employees = new List<Employee>();
            List<Employee> employeesOfDep = new List<Employee>();
            List<int> listId = new List<int>();
            listId.Add(id);
            List<int> listIdDepartmnet = await departmentService.GetAllIdDepartmnet(departmentRes, listId);

            foreach (int idDe in listIdDepartmnet)
            {
                employeesOfDep = await employeeService.GetEmployeesByDerpartmentId(idDe);
                if (employeesOfDep != null)
                {
                    employees.AddRange(employeesOfDep);
                }
            };

            employees = employees.OrderBy(e => e.Id).ToList();

            return employeeService.ToEmployeeList(employees) ;
        }
     

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
        [HttpPost("/api/searchBy")]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> GetAllEmployeesSearchBy([FromBody] SearchObject searchObject)

        {
            List<Employee> employeesSearch = new List<Employee>();
            List<DepartmentRes> departmentRes = await departmentService.GetDepartmentsTreeById(searchObject.DepartmentId);

            List<Employee> employees = await employeeService.GetEmployeesSearchBy(searchObject.Field, searchObject.Operator, searchObject.Value);

            List<int> listId = new List<int>();
            listId.Add(searchObject.DepartmentId);
            List<int> listIdDepartmnet = await departmentService.GetAllIdDepartmnet(departmentRes, listId);

            foreach (int id in listId)
            {
                var employeesOfDep = employees.Where(e => e.Department.Id == id).ToList();
                if (employeesOfDep != null)
                {
                    employeesSearch.AddRange(employeesOfDep);
                }
            };


            return employeeService.ToEmployeeList(employeesSearch);
            //return employeesSearch;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<EmployeeView>> PostEmployee([FromBody] EmployeeRequest employeeRequest)
        {

            Department department = await departmentService.GetDepartmentById(employeeRequest.DepartmentId);

            Employee employee = new Employee();
            if (department != null)
            {
                employee.FirstName = employeeRequest.FirstName;
                employee.LastName = employeeRequest.LastName;
                employee.AvatarPath = employeeRequest.AvatarPath;
                employee.Position = employeeRequest.Position;
                employee.Title = employeeRequest.Title;
                employee.Deleted = false;
                employee.Department = department;
            }
            else
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                await employeeService.Create(employee);

                return CreatedAtAction("GetEmployee", new { id = employee.Id }, employeeService.ToEmployee(employee));
            }
            return BadRequest();
        }


      
        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, [FromBody] EmployeeRequest employeeRequest)
        {
            if (id != employeeRequest.EmployeeId)
            {
                return BadRequest();
            }
            Department department = await departmentService.GetDepartmentById(employeeRequest.DepartmentId);

            Employee employee = await employeeService.GetEmployeeById(id);
            if (department != null && employee != null)
            {
                employee.FirstName = employeeRequest.FirstName;
                employee.LastName = employeeRequest.LastName;
                employee.AvatarPath = employeeRequest.AvatarPath;
                employee.Position = employeeRequest.Position;
                employee.Title = employeeRequest.Title;
                employee.AvatarPath = employeeRequest.AvatarPath;
                employee.Deleted = false;
                employee.Department = department;
            }
            else
            {
                return BadRequest();
            }

            try
            {
                await employeeService.Modify(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeService.EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("/api/pageChange")]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> PageChange([FromBody] PageChange pageChange)
        {
            List<Employee> employees = new List<Employee>();

            if (pageChange.DepartmentId != 0)
            {
                List<DepartmentRes> departmentRes = await departmentService.GetDepartmentsTreeById(pageChange.DepartmentId);

                List<Employee> employeesOfDep = new List<Employee>();
                List<int> listId = new List<int>();
                listId.Add(pageChange.DepartmentId);
                List<int> listIdDepartmnet = await departmentService.GetAllIdDepartmnet(departmentRes, listId);

                foreach (int idDe in listIdDepartmnet)
                {
                    employeesOfDep = await employeeService.GetEmployeesByDerpartmentId(idDe);
                    if (employeesOfDep != null)
                    {
                        employees.AddRange(employeesOfDep);
                    }
                };
            }else
            {
                employees = await employeeService.GetEmployees();
            }
            employees = employees.OrderBy(e => e.Id).ToList();

            return employeeService.ToEmployeeList(employees).Skip(pageChange.Skip).Take(pageChange.Take).ToList();
        }


            // DELETE api/<EmployeeController>/5
            [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            await employeeService.Remove(id);
            return employee;
        }


    }
}
