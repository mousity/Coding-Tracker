using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static coding_tracker.Enums;
using static coding_tracker.DataController;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace coding_tracker
{
    internal class UserInterface
    {
        private DataController _controller = new DataController();
        internal void MainMenu()
        {


            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                    .Title("Select an option:")
                    .AddChoices(MenuOptions.ViewAllCodingSessions,
                                MenuOptions.AddCodingSession,
                                MenuOptions.DeleteCodingSession,
                                MenuOptions.UpdateCodingSession,
                                MenuOptions.ExitProgram)
            );

            switch (choice)
            {
                case MenuOptions.ViewAllCodingSessions:
                    // View all coding sessions
                    break;
                case MenuOptions.AddCodingSession:
                    // Add a coding session
                    _controller.AddSession();
                    break;
                case MenuOptions.DeleteCodingSession:
                    // Delete a coding session
                    break;
                case MenuOptions.UpdateCodingSession:
                    // Update a coding session
                    break;
                case MenuOptions.ExitProgram:
                    Console.WriteLine("Goodbye!");
                    break;
            }


        }
    }
}
