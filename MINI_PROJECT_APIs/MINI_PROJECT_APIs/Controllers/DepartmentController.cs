using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MINI_PROJECT_APIs.Data;
using MINI_PROJECT_APIs.Domain.Responses.Department;
using MINI_PROJECT_APIs.Models;
using MINI_PROJECT_APIs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MINI_PROJECT_APIs.Controllers
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
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await this.departmentService.GetDepartments();
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
