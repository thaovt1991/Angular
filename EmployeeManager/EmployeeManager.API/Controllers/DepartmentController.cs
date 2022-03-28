using EmployeeManager.DAL.Models;
using EmployeeManager.DAL.Service.IService;
using EmployeeManager.Domain.Responses.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentView>>> GetDepartments()
        {
            List<Department> departments = await departmentService.GetDepartments();
            return departmentService.ToDepartmentList(departments);
            //return await departmentService.GetDepartments();
        }

        [HttpGet("/api/showTree")]
        public async Task<ActionResult<IEnumerable<DepartmentRes>>> GetDepartmentsTreeDepartmentRes()
        {
            return await departmentService.GetDepartmentsTreeById(0);
        }

        [HttpPost("/api/showTree/searchDepartmentBy")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<List<DepartmentRes>>> GetDepartmentsTreeDepartmentResSearchBy([FromForm] string keywords)
        {
            List<Department> childrent = await departmentService.SearchDepartmentByKeyword(keywords);

            if (childrent.Count == 0)
            {
                return await departmentService.GetDepartmentsTreeById(0);
            }
            List<DepartmentRes> departmentsTreeSearch = await departmentService.DepartmentTreeBySearch(childrent);

            return departmentsTreeSearch;
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await departmentService.GetDepartmentById(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }
    }
}
