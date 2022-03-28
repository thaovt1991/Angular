using MINI_PROJECT_APIs.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MINI_PROJECT_APIs.Service
{
    public class BaseService
    {
        protected IDbConnection conn;
        public BaseService()
        {
            conn = new SqlConnection(Common.ConnectionString);
        }
    }
}
