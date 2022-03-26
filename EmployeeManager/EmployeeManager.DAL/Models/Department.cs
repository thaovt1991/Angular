using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        [MaxLength(200, ErrorMessage = "Tên phòng ban không quá 200 từ")]
        public string Name { get; set; }

        public int ParentId { get; set; }

        public bool Deleted { get; set; }
        [JsonIgnore]
        public virtual IList<Employee> Employees { get; set; }

        public static implicit operator List<object>(Department v)
        {
            throw new NotImplementedException();
        }
    }
}
