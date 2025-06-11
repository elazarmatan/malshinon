using System;

static class Menu
{
    public static  void enterToSystem()
    {
        PersonDal dal = new PersonDal();
        Console.WriteLine(
               "======================================\n" +
               "         WELCOME TO MALSHINON\n" +
               "======================================\n"
                 );

        Console.WriteLine(
            "How do you want to enter?\n" +
            "1. ful name\n" +
            "2. secrete code\n"
            );

        string selectName = Console.ReadLine();
        switch (selectName)
        {
            case "1":
                Console.WriteLine("enter first name");
                string firstName = Console.ReadLine();
                Console.WriteLine("enter last name");
                string lastName = Console.ReadLine();
                if (dal.GetPersonByName(firstName, lastName))
                {
                    Console.WriteLine("YOU ARE INSIDE!!");
                }
                else
                {
                    Console.WriteLine("You do not exist in the system, a new user has been created for you!");
                    string scr = dal.createSecretCode(firstName);
                    persons person = new persons(firstName, lastName, scr);
                    dal.InsertNewPerson(person);
                }
                break;
            case "2":
                Console.WriteLine("enter secret code");
                string secretCode = Console.ReadLine();
                if (dal.GetPersonBySecretCode(secretCode))
                {
                    Console.WriteLine("YOU ARE INSIDE!!");
                }
                else
                {
                    Console.WriteLine("you do not exist in the system please enter first name: ");
                    string firstName1 = Console.ReadLine();
                    Console.WriteLine("enter last name");
                    string lastName2 = Console.ReadLine();
                    persons pers = new persons(firstName1, lastName2, secretCode);
                    dal.InsertNewPerson(pers);
                }
                break;

        }
    }

    public static void createReport()
    {
        PersonDal dal = new PersonDal();
        Console.WriteLine("enter first name");
        string firstName = Console.ReadLine();
        Console.WriteLine("enter last name");
        string lastName = Console.ReadLine();
        Console.WriteLine("enter text");
        string text = Console.ReadLine();
        fullName name1 = dal.NameExtraction(text);
        string first = name1.firstName;
        string last = name1.lastName;
        if (!dal.GetPersonByName(first, last))
        {
            string scr = dal.createSecretCode(last);
            if (!dal.SecretCodeExists(scr))
            {
                persons person = new persons(first, last, scr, type.target);
                dal.InsertNewPerson(person);
            }

        }
        int idtarg = dal.getIdByName(first, last);
        int idrep = dal.getIdByName(firstName, lastName);
        Reports rep = new Reports(text, idrep, idtarg);
        dal.InsertIntelReport(rep);
        dal.UpdateMentionCount(idtarg, 1);
        dal.UpdateReportCount(1, idrep);
    }
}