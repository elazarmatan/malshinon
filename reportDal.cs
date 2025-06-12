using System;
using MySql.Data.MySqlClient;

public class ReportDal
{
    string constr = "server=localhost;username=root;password=;database=malshinon";
    private MySqlConnection _con;

    private void createConnection()
    {
        this._con = new MySqlConnection(constr);

    }
    private MySqlCommand comand(string query)
    {
        MySqlCommand cmd = new MySqlCommand(query, _con);
        return cmd;
    }

    public ReportDal()
    {
        this.createConnection();
    }
    public void UpdateReportCount(int id)
    {
        try
        {
            _con.Open();
            string query = $"UPDATE persons SET num_reports = num_reports + 1 WHERE id = @id";

            var cmd = comand(query);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e.Message);
        }
        finally
        {
            _con.Close();
        }
    }
    public void UpdateMentionCount(int id)
    {
        try
        {
            _con.Open();
            string query = $"UPDATE persons SET num_mentions = num_mentions +1 WHERE id = @id";

            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e.Message);
        }
        finally
        {
            _con.Close();
        }
    }
    public void InsertIntelReport(Reports rep)
    {
        try
        {
            _con.Open();
            string query = "INSERT INTO reports(reporter_id,target_id,text) VALUES (@reporter_id,@target_id,@text)";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@reporter_id", rep.reportsId);
            cmd.Parameters.AddWithValue("@target_id", rep.targetId);
            cmd.Parameters.AddWithValue("@text", rep.text);

            cmd.ExecuteNonQuery();
            Console.WriteLine("INSERT REPORT SUCESS!!!");
        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e.Message);
        }
        finally
        {
            _con.Close();
        }
    }
    public int getNumReports(string firstName, string lastName)
    {
        int nr = 0;
        try
        {

            _con.Open();
            string query = "SELECT num_reports FROM persons WHERE first_name=@first_name AND last_name= @last_name";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@first_name", firstName);
            cmd.Parameters.AddWithValue("@last_name", lastName);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                nr = reader.GetInt32("num_reports");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e.Message);
        }
        finally
        {
            _con.Close();
        }
        return nr;
    }


    //public void Printreport()
    //{
    //    PersonDal dal = new PersonDal();


    //    Console.WriteLine("========= Person Details =========");
    //    Console.WriteLine($"Reporter:        {first_name}");
    //    Console.WriteLine($"Target:          {last_name}");
    //    Console.WriteLine($"Text:            {}");
    //    Console.WriteLine($"Type:            {type}");
    //    Console.WriteLine($"Time to report:  {num_reports}");
    //    Console.WriteLine("==================================");
    //}

}