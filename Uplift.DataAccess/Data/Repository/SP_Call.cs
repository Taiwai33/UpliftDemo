using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Uplift.DataAccess.Data.Repository.IRepository;
using UpliftUdemy.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        //create sql connection
        private readonly ApplicationDbContext _db;
        private static string ConnectionString = "";

        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString; //retrieve conn string form appDbContext
        }
       

        public T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure),typeof(T));//this will be a stored proc call

            }
        }

        public void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);//this will be a stored proc call

            }
        }

        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);//calls stored proc to retrieve list of categories
            
            }
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
