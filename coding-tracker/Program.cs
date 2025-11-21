using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=coding_tracker.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
            }
        }
    }
}