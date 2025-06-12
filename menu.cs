using System;
using System.Collections.Generic;

static class Menu
{
    //הפונקצייה מאפשרת כניסה למערכת באמצעות השם
    public static void enterByName()
    {
        PersonDal dal = new PersonDal();
        LogDal ldal = new LogDal();
        ReportDal rdal = new ReportDal();


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
            else
            {
                if (rdal.getNumReports(firstName, lastName) >= 10)
                {
                    dal.updateType(id, "potential_agent");
                    ldal.createLog($"the type of {firstName} {lastName} change to potential agent");
                }
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

    //הפונקצייה מאפשרת כניסה למערכת באמצעות קוד סודי
    public static void enterBySecretCode()
    {
        PersonDal dal = new PersonDal();
        LogDal ldal = new LogDal();
        ReportDal rdal = new ReportDal();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Enter secret code: ");
        string secretCode = Console.ReadLine();

        if (dal.GetPersonBySecretCode(secretCode))
        {
            int id = dal.getIdBySecretCode(secretCode);
            if (dal.getType("target", id))
            {
                dal.updateType(id, "both");

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


    //הפונקצייה מאפשרת יצירת דיווח
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
                ldal.createLog($"the type of {firstName} {lastName} change to both");

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

   
    //פונקציה שבודקת סיסמה ומאפשרת כניסה למנהל בלבד
    public static bool entryManager(string password)
    {
        bool entry = false;
        if (password == "12345678")
        {
            entry = true;
        }
        return entry;
    }
    //פונקצייה שמציגה את תפריט המנהל
    public static void menuManager()
    {
        LogDal ldal = new LogDal();
        ManagerDal dal = new ManagerDal();
        bool exit = true;

        while (exit)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================");
            Console.WriteLine("    MENU MANAGER");
            Console.WriteLine("====================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. list logs");
            Console.WriteLine("2. list reporters");
            Console.WriteLine("3. list targets");
            Console.WriteLine("4. list both");
            Console.WriteLine("5. list potential agents");
            Console.WriteLine("6. list reports");
            Console.WriteLine("7. exit");
            Console.ResetColor();

            string select = Console.ReadLine();
            switch (select)
            {
                case "1":
                    List<Log> logs = ldal.getAllLogs();
                    foreach (Log l in logs)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        l.ToString();
                        Console.ResetColor();
                    }
                    break;
                case "2":
                    List<persons> persr = dal.GetPerson("reporter");
                    foreach (persons p in persr)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        p.PrintPersonDetails();
                        Console.ResetColor();
                    }
                    break;
                case "3":
                    List<persons> perst = dal.GetPerson("target");
                    foreach (persons p in perst)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        p.PrintPersonDetails();
                        Console.ResetColor();
                    }
                    break;
                case "4":
                    List<persons> persb = dal.GetPerson("both");
                    foreach (persons p in persb)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        p.PrintPersonDetails();
                        Console.ResetColor();
                    }
                    break;
                case "5":
                    List<persons> persp = dal.GetPerson("potential_agent");
                    foreach (persons p in persp)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        p.PrintPersonDetails();
                        Console.ResetColor();
                    }
                    break;
                case "6":
                    break;
                case "7":
                    exit = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please choose a valid menu number.");
                    Console.ResetColor();
                    break;
            }
        }
    }

}