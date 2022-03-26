using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ nhân viên không được để trống")]
        [MaxLength(200, ErrorMessage = "Họ nhân viên không quá 200 từ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [MaxLength(200, ErrorMessage = "Tên nhân viên không quá 200 từ")]
        public string LastName { get; set; }

        public bool Deleted { get; set; }

        public string Position { get; set; }// chức vụ

        public string Title { get; set; } //chuc danh 

        public string AvatarPath { get; set; }

        public virtual Department Department { get; set; }
    }
}
