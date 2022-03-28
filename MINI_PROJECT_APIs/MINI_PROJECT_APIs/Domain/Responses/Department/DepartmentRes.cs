using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MINI_PROJECT_APIs.Domain.Responses.Department
{
    public class DepartmentRes
    {
       
        public int Id { get; set; }
        public string Name { get; set; }

        public int ParentId { get; set; }
        //public bool Deleted { get; set; }
     
        public  List<DepartmentRes> Children { get; set; }
    }
}
