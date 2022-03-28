using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Responses.Other
{
    public class PageChange
    {
        public int DepartmentId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
