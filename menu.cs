using System;

static class Menu
{
    public static void enterByName()
    {
        PersonDal dal = new PersonDal();
        LogDal ldal = new LogDal();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        if (dal.GetPersonByName(firstName, lastName))
        {
            int id = dal.getIdByName(firstName, lastName);
            if (dal.getType("target", id))
            {
                dal.updateType(id,"both");
                ldal.createLog($"the type of {firstName} {lastName} change to both");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Access granted.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("User not found.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Creating new user...");
            string fl = firstName + lastName;
            string scr = dal.createSecretCode(fl);
            
            persons person = new persons(firstName, lastName, scr,"reporter");
            dal.InsertNewPerson(person);
            ldal.createLog($"create new person named {firstName} {lastName}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Generated secret code: {scr}");
        }

        Console.ResetColor();
    }


    public static void enterBySecretCode()
    {
        PersonDal dal = new PersonDal();
        LogDal ldal = new LogDal();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Enter secret code: ");
        string secretCode = Console.ReadLine();

        if (dal.GetPersonBySecretCode(secretCode))
        {
            int id = dal.getIdBySecretCode(secretCode);
            if (dal.getType("target", id))
            {
                dal.updateType(id, "both");
                //ldal.createLog($"the type of {} change to both");

            }
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
            persons pers = new persons(firstName, lastName, secretCode,"reporter");
            dal.InsertNewPerson(pers);
            ldal.createLog($"create new person named {firstName} {lastName}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("New user created successfully.");
        }

        Console.ResetColor();
    }



    public static void createReport()
    {
        PersonDal dal = new PersonDal();
        ReportDal rdal = new ReportDal();
        LogDal ldal = new LogDal();

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
            string fl = first + last;
            string scr = dal.createSecretCode(fl);
            if (!dal.SecretCodeExists(scr))
            {
                persons person = new persons(first, last, scr,"target");
                dal.InsertNewPerson(person);
                Console.ForegroundColor = ConsoleColor.Cyan;
                ldal.createLog($"New target created named {first} {last}");
            }
        }
        else
        {
            int id = dal.getIdByName(firstName, lastName);
            if (dal.getType("reporter", id))
            {
                dal.updateType(id, "both");
                ldal.createLog($"the type of {} change to both");

            }
        }

            int idtarg = dal.getIdByName(first, last);
        int idrep = dal.getIdByName(firstName, lastName);

        Reports rep = new Reports(text, idrep, idtarg);
        rdal.InsertIntelReport(rep);
        ldal.createLog($"new report {firstName} {lastName} The informant to {first} {last}");
        rdal.UpdateMentionCount(idtarg);
        rdal.UpdateReportCount(idrep);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Report submitted successfully.");

        Console.ResetColor();
    }
}