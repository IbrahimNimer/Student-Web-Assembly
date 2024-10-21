using LazurdIT.FluentOrm.MsSql;
using System.Data.SqlClient;
using Student.Shared;

namespace Student.Server.Repository
{
    public class StudentContext : MsSqlFluentRepository<StudentModel>
    {
        public StudentContext(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}