using Microsoft.Data.SqlClient;

namespace UserAdministrator.Data.Interfaces
{
    public interface IAppContext
    {
        SqlConnection GetConnection();
    }
}
