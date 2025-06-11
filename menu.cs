using System;

static class Menu
{
    public static void enterByName()
    {
        PersonDal dal = new PersonDal();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        if (dal.GetPersonByName(firstName, lastName))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Access granted.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("User not found.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Creating new user...");
            string scr = dal.createSecretCode(firstName);
            string typer = "";
            if (dal.getType("reporter", firstName, lastName))
            {
                typer = "reporter";
            }
            else
            {
                typer = "both";
            }
            persons person = new persons(firstName, lastName, scr,typer);
            dal.InsertNewPerson(person);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Generated secret code: {scr}");
        }

        Console.ResetColor();
    }


    public static void enterBySecretCode()
    {
        PersonDal dal = new PersonDal();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Enter secret code: ");
        string secretCode = Console.ReadLine();

        if (dal.GetPersonBySecretCode(secretCode))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Access granted.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Code not recognized.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Please enter your name to register.");
            Console.Write("First name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = Console.ReadLine();
            string typer = "";
            if (dal.getType("reporter", firstName, lastName))
            {
                typer = "reporter";
            }
            else
            {
                typer = "both";
            }
            persons pers = new persons(firstName, lastName, secretCode,typer);
            dal.InsertNewPerson(pers);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("New user created successfully.");
        }

        Console.ResetColor();
    }



    public static void createReport()
    {
        PersonDal dal = new PersonDal();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Enter your first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter your last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter report text: ");
        string text = Console.ReadLine();

        fullName name1 = dal.NameExtraction(text);
        string first = name1.firstName;
        string last = name1.lastName;

        if (!dal.GetPersonByName(first, last))
        {
            string scr = dal.createSecretCode(last);
            if (!dal.SecretCodeExists(scr))
            {
                persons person = new persons(first, last, scr,"target");
                dal.InsertNewPerson(person);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"New target created with code: {scr}");
            }
        }

        int idtarg = dal.getIdByName(first, last);
        int idrep = dal.getIdByName(firstName, lastName);

        Reports rep = new Reports(text, idrep, idtarg);
        dal.InsertIntelReport(rep);

        dal.UpdateMentionCount(idtarg, 1);
        dal.UpdateReportCount(1, idrep);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Report submitted successfully.");

        Console.ResetColor();
    }
}