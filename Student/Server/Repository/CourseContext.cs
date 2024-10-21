using LazurdIT.FluentOrm.MsSql;
using Student.Shared;
using System.Data.SqlClient;

namespace Student.Server.Repository
{
    public class CourseContext : MsSqlFluentRepository<CourseModel>
    {
        public CourseContext(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}
