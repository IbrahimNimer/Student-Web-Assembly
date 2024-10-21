using LazurdIT.FluentOrm.MsSql;
using Student.Shared;
using System.Data.SqlClient;

namespace Student.Server.Repository
{
    public class EnrollmentContext : MsSqlFluentRepository<EnrollmentModel>
    {
        public EnrollmentContext(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}
