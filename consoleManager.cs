using System;

public class ConsoleManager
{
    public ConsoleManager()
    {
        PersonDal dal = new PersonDal();
        bool v = true;
        while (v)
        {

            Menu.enterToSystem(); 
            v = false;
        }
        bool exit = true;
        while (exit)
        {
          

            Console.WriteLine(
                "      MENU\n" +
                "________________\n" +
                "1.create report \n" +
                "2.exit"
                );
            string selectChoice = Console.ReadLine();
            switch (selectChoice)
            {
                case "1":
                    Menu.createReport();
                    break;
                case "2":
                    exit = false;
                    break;
            }
        }
    }
}


