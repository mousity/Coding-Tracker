using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;
using Spectre.Console;
using System.Configuration;
using System.Data.SQLite;

namespace coding_tracker
{

    internal class DataController
    {
        private const string dateTimeFormat = "yyyy-MM-dd HH:mm";
        public static readonly string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private static readonly CultureInfo inv = CultureInfo.InvariantCulture; // Invariant culture for consistent date formatting

        // Method to add a coding session
        internal void AddSession()
        {
            DateTime? startTime = null;
            DateTime? endTime = null;

            Console.Clear();

            // While startTime is null, keep prompting the user for input
            while (startTime == null)
            {
                AnsiConsole.MarkupLine("Adding a coding session. Please input the [green]start date[/] and [cyan]time[/] of the session (YYYY-MM-DD HH:MM):");
                AnsiConsole.MarkupLine("Type [red]0[/] to go back to the main menu");
                var input = AnsiConsole.Ask<string>("Start Date and Time: ");

                if (input.Trim() == "0") { return; } // If user inputs '0', return to main menu
                startTime = ParseDateTime(input); // Try to parse the input

            }

            // While endTime is null, keep prompting the user for input
            while (endTime == null)
            {
                AnsiConsole.MarkupLine("\nPlease input the [green]end date[/] and [cyan]time[/] of the session (YYYY-MM-DD HH:MM):");
                AnsiConsole.MarkupLine("Type [red]0[/] to go back to the main menu");
                var input = AnsiConsole.Ask<string>("End Date and Time: ");

                if (input.Trim() == "0") { return; } // If user inputs '0', return to main menu
                endTime = ParseDateTime(input); // Try to parse the input
            }

            // Validate that endTime is after startTime
            if (endTime.Value < startTime.Value)
            {
                Console.WriteLine("End time, " + endTime.Value + " must be after start time, " + startTime.Value + ". Please try again.");
                Console.ReadKey();
                return;
            }

            // Calculate duration in minutes
            var duration = Convert.ToInt32((endTime.Value - startTime.Value).TotalMinutes);

            // Get connection string from config
            using var connection = new SQLiteConnection(connectionString); // SQLite connection (Dapper does not require manual open/close)

            // The SQL insert statement
            var sql = "INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";
            // The actual execution of the insert statement
            connection.Execute(sql, new
            {
                StartTime = startTime.Value.ToString(dateTimeFormat, inv),
                EndTime = endTime.Value.ToString(dateTimeFormat, inv),
                Duration = duration
            });

        }

        internal DateTime? ParseDateTime(string input)
        {
            DateTime parsedDateTime;
            bool success = DateTime.TryParseExact(
                input, // User input
                dateTimeFormat, // Expected format
                CultureInfo.InvariantCulture, // Culture info
                DateTimeStyles.None, // No special styles
                out parsedDateTime // Output variable
            );
            if (!success)
            {
                Console.WriteLine("Invalid date and time format. Please use YYYY-MM-DD HH:MM.");
                return null;
            }
            return parsedDateTime;
        }

        // Method to view all coding sessions
        internal void ViewAllSessions()
        {
            // Implementation for viewing all sessions will go here
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration (minutes)");

            using var connection = new SQLiteConnection(connectionString);
            var sql = "SELECT * FROM coding_tracker";
            var sessions = connection.Query(sql).ToList(); // FIX: Call ToList() as a method

            foreach (var session in sessions)
            {
                Console.WriteLine(session);
            }
        }

        // Method to delete a coding session
        internal void DeleteSession()
        {
            var sessionId = AnsiConsole.Ask<int>("Enter the ID of the session to delete: ");

            using var connection = new SQLiteConnection(connectionString);
            var sql = "DELETE FROM coding_tracker WHERE Id = @Id";
            var rows = connection.Execute(sql, new { Id = sessionId });

            if(rows > 0)
            {
                AnsiConsole.MarkupLine($"[green]Session with ID {sessionId} deleted successfully.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No session found with ID {sessionId}.[/]");
            }

            return;
        }

        // Method to update a coding session
        internal void UpdateSession()
        {
            // Implementation for updating a session will go here

        }
    }
}
