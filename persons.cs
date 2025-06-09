
public enum type
{
    reporter,
    target,
    both,
    potential_agent
}
public class persons
{
    int id { get; set; }
    string first_name { get; set; }
    string last_name { get; set; }
    string secret_code { get; set; }
    type type { get; set; } = type.reporter;
    int   num_reports { get; set; }
    int    num_mentions { get; set; }

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
}