using System.Data.SqlClient;

namespace BooksWebApi.DataAccess.Data
{
    public interface IConnectionService
    {
        SqlConnection GetConnection();
        void Open();
        void Close();
    }
}
