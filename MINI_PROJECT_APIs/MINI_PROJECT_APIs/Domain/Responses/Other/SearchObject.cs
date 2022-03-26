using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MINI_PROJECT_APIs.Domain.Responses.Other
{
    public class SearchObject
    {
        public int DepartmentId { get; set; }
        public string Field { get; set;}
        public string Operator  { get; set; }
        public string Value { get; set; }
    }
}
