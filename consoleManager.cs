using System;

public class ConsoleManager
{
    public ConsoleManager()
    {
        PersonDal dal = new PersonDal();
        bool exit = true;
        while(exit)
        {
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
                        persons person = new persons(firstName,lastName,scr);
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
                        persons pers = new persons(firstName1,lastName2,secretCode);
                        dal.InsertNewPerson(pers);
                    }
                    break;
            }

            Console.WriteLine(
                "      MENU\n"+
                "________________\n" +
                "1.create report \n" +
                "2.exit"
                );
            string selectChoice = Console.ReadLine();
            switch (selectChoice)
            {
                case "1":
                    Console.WriteLine("enter text");
                    break;
                case "2":
                    exit = false;
                    break;
            }
        }
    }


}