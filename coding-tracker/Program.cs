using Microsoft.Data.Sqlite;
using Dapper;
using System.Configuration;
using System.Collections.Specialized;
using static coding_tracker.DatabaseManager;


    namespace coding_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();


        }
    }
}