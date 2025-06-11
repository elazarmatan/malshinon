
using System;

public enum type
{
    reporter,
    target,
    both,
    potential_agent
}
public class persons
{
    public int id { get; set; }

    public string first_name { get; set; }
    public string last_name { get; set; }
    public string secret_code { get; set; }
    public type type { get; set; } = type.reporter;
    public int   num_reports { get; set; }
    public int    num_mentions { get; set; }

    public persons(int id ,string first_name,string last_name,string secret_code,type type,int num_reports,int num_mentions)
    {
        this.id = id;
        this.first_name = first_name;
        this.last_name = last_name;
        this.secret_code = secret_code;
        this.type = type;
        this.num_reports = num_reports;
        this.num_mentions = num_mentions;
    }

    

    public persons(string first_name, string last_name, string secret_code)
    {
        this.first_name = first_name;
        this.last_name = last_name;
        this.secret_code = secret_code;
    }
    public void PrintPersonDetails()
    {
        Console.WriteLine("========= Person Details =========");
        Console.WriteLine($"ID:              {id}");
        Console.WriteLine($"First Name:      {first_name}");
        Console.WriteLine($"Last Name:       {last_name}");
        Console.WriteLine($"Secret Code:     {secret_code}");
        Console.WriteLine($"Type:            {type}");
        Console.WriteLine($"# of Reports:    {num_reports}");
        Console.WriteLine($"# of Mentions:   {num_mentions}");
        Console.WriteLine("==================================");
    }

}