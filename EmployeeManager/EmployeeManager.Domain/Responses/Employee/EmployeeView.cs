using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Responses.Employee
{
    public class EmployeeView
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public bool Deleted { get; set; }
        public string Position { get; set; }// chức vụ
        public string Title { get; set; } //chuc danh 
        public string AvatarPath { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}
