using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MINI_PROJECT_APIs.Domain.Request
{
    public class EmployeeRequest
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }// chức vụ
        public string Title { get; set; } //chuc danh 
        public string AvatarPath { get; set; }
        //public IFormFile AvatarPath { get; set; }
        public int DepartmentId { get; set; }
    }
}
