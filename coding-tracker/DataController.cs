using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;
using Spectre.Console;

namespace coding_tracker
{
    internal class DataController
    {
        const string dateTimeFormat = "yyyy-MM-dd HH:mm";

        internal void AddSession()
        {
            DateTime? startTime = null;
            DateTime? endTime = null;

            // Implementation for adding a coding session
            while (startTime == null)
            {
                AnsiConsole.MarkupLine("Adding a coding session. Please input the [green]start date[/] and [cyan]time[/] of the session (YYYY-MM-DD HH:MM):");
                AnsiConsole.MarkupLine("Type [red]0[/] to go back to the main menu");
                var input = AnsiConsole.Ask<string>("Start Date and Time: ");
                if (input.Trim() == "0")
                {
                    return;
                }
                startTime = ParseDateTime(input);
                
            }

            while(endTime == null)
            {
                AnsiConsole.MarkupLine("\nPlease input the [green]end date[/] and [cyan]time[/] of the session (YYYY-MM-DD HH:MM):");
                AnsiConsole.MarkupLine("Type [red]0[/] to go back to the main menu");
                var input = AnsiConsole.Ask<string>("End Date and Time: ");
                if(input.Trim() == "0")
                {
                    return;
                }
                endTime = ParseDateTime(input);
            }

            if (endTime.Value > startTime.Value)
            {
                Console.WriteLine(startTime.Value + " is your start time and " + endTime.Value + " is your end time");
            } else
            {
                Console.WriteLine("End time, " + endTime.Value + " must be after start time, " + startTime.Value + ". Please try again.");
            }
            
            var duration = Convert.ToInt32((endTime.Value - startTime.Value).TotalMinutes);
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




    }
}
