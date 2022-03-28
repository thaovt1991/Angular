using EmployeeManager.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.DAL.Repository.Repository
{
    public class BaseRepository
    {
        protected IDbConnection conn;

        public BaseRepository()
        {
            conn = new SqlConnection(Common.ConnectionString);
        }
    }
}
