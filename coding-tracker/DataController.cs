using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;

namespace coding_tracker
{
    internal class DataController
    {
        const string dateTimeFormat = "yyyy-MM-dd HH:mm";

        internal void AddSession()
        {
            DateTime result;
            // Implementation for adding a coding session
            Console.WriteLine("Adding a coding session. Please input the start date and time of the session (YYYY-MM-DD HH:MM):");
            bool success = DateTime.TryParseExact( // Try to parse the date exactly
                Console.ReadLine(), // User input
                dateTimeFormat, // Expected format
                CultureInfo.InvariantCulture, // Culture info
                DateTimeStyles.None, // No special styles
                out result // Output variable
            );

            if(!success)
            {
                Console.WriteLine("Invalid date and time format. Please use YYYY-MM-DD HH:MM.");
                return;
            }
        }
    }
}
