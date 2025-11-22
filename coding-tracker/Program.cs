using Microsoft.Data.Sqlite;
using Dapper;
using System.Configuration;
using System.Collections.Specialized;
using static coding_tracker.DatabaseManager;
using static coding_tracker.UserInterface;


namespace coding_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInterface userInterface = new();
            CreateDatabase();

            userInterface.MainMenu();

        }
    }
}