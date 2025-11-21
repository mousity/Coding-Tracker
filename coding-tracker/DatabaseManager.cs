using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Configuration;

namespace coding_tracker
{
    internal class DatabaseManager
    {
        public static void CreateDatabase()
        {
            string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is not configured.");
            }


            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            connection.Execute(@"
                            CREATE TABLE IF NOT EXISTS coding_tracker (
                              Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                              StartTime TEXT NOT NULL,
                              EndTime   TEXT NOT NULL,
                              Duration  INTEGER NOT NULL
                            );
            ");

            Console.WriteLine("Database created if it did not already exist");
        }
    }
}
