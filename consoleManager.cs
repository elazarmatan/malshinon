using System;

public class ConsoleManager
{
    public ConsoleManager()
    {
        PersonDal dal = new PersonDal();
        bool v = true;
        while (v)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================");
            Console.WriteLine("         WELCOME TO MALSHINON");
            Console.WriteLine("======================================\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("How would you like to log in?");
            Console.WriteLine("1. Full name");
            Console.WriteLine("2. Secret code");
            Console.WriteLine("3. i am a manager");

            Console.ResetColor();
            Console.Write("Enter your choice (1 or 2): ");
            string selectName = Console.ReadLine();
            Console.WriteLine();

            switch (selectName)
            {
                case "1":
                    Menu.enterByName();
                    break;
                case "2":
                    Menu.enterBySecretCode();
                    break;
                case "3":
                    Console.WriteLine("enter password");
                    string password = Console.ReadLine();
                    if (Menu.entryManager(password))
                    {
                        Menu.menuManager();
                        v = false;
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ResetColor();
                    continue;
            }

            v = false;
        }

        bool exit = true;
        while (exit)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n========= MAIN MENU =========");
            Console.WriteLine("1. Create a report");
            Console.WriteLine("2. show my details");
            Console.WriteLine("3. Exit");
            Console.ResetColor();

            Console.Write("Enter your choice: ");
            string selectChoice = Console.ReadLine();
            Console.WriteLine();

            switch (selectChoice)
            {
                case "1":
                    Menu.createReport();
                    break;
                case "2":
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Exiting... Goodbye!");
                    Console.ResetColor();
                    exit = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}


