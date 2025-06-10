using System;
using System.Xml.Linq;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

public class PersonDal
{
    string constr = "server=localhost;username=root;password=;database=malshinon";
    private MySqlConnection _con;

    private void createConnection()
    {
        this._con = new MySqlConnection(constr);

    }

    public PersonDal()
    {
        this.createConnection();
    }

    private MySqlCommand comand(string query)
    {
        MySqlCommand cmd = new MySqlCommand(query, _con);
        return cmd;
    }

    public persons GetPersonByName(string name) 
    {
        persons pers = new persons();
        try
        {
            _con.Open();
            Console.WriteLine("CONECTION SUCESS\n");
            string query = "SELECT * FROM persons WHERE first_name=@name";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@name",name);
            var reader = cmd.ExecuteReader();
            var typeString = reader.GetString(reader.GetOrdinal("type"));
            type type = (type)Enum.Parse(typeof(type), typeString, true);
            while (reader.Read())
            {

                pers.id = reader.GetInt16("id");
                pers.first_name = reader.GetString("first_name");
                pers.last_name = reader.GetString("last_name");
                pers.secret_code = reader.GetString("secrete_code");
                pers.type = type;
                pers.num_reports = reader.GetInt16("num_reports");
                pers.num_mentions = reader.GetInt16("num_mentions");
                
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("EROR: "+e);
        }
        finally
        {
            _con.Close();
        }
        return pers;
    }
    public persons GetPersonBySecretCode(string secreteCode) 
    {
        persons pers = new persons();
        try
        {
            _con.Open();
            Console.WriteLine("CONECTION SUCESS\n");
            string query = "SELECT * FROM persons WHERE secret_code=@secret_code";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@secret_code", secreteCode);
            var reader = cmd.ExecuteReader();
            var typeString = reader.GetString(reader.GetOrdinal("type"));
            type type = (type)Enum.Parse(typeof(type), typeString, true);
            while (reader.Read())
            {

                pers.id = reader.GetInt16("id");
                pers.first_name = reader.GetString("first_name");
                pers.last_name = reader.GetString("last_name");
                pers.secret_code = reader.GetString("secrete_code");
                pers.type = type;
                pers.num_reports = reader.GetInt16("num_reports");
                pers.num_mentions = reader.GetInt16("num_mentions");

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e);
        }
        finally
        {
            _con.Close();
        }
        return pers;
    }
    public void InsertNewPerson(persons pers) 
    {
        try
        {
            _con.Open();
            Console.WriteLine("CONECTION SUCESS\n");
            string query = "INSERT INTO persons (first_name,last_name,secrete_code) VALUES(@first_name,@last_name,@secrete_code)";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@first_name",pers.first_name);
            cmd.Parameters.AddWithValue("@last_name",pers.last_name);
            cmd.Parameters.AddWithValue("@secrete_code",pers.secret_code);

            cmd.ExecuteNonQuery();
            Console.WriteLine("INSERT SUCESS!!!");
        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e);
        }
        finally
        {
            _con.Close();
        }
    }
    public void InsertIntelReport(int reporter_id,int target_id ,string  text) 
    {
        try
        {
            _con.Open();
            Console.WriteLine("CONECTION SUCESS\n");
            string query = "INSERT INTO reports(reporter_id,target_id,text) VALUES (@reporter_id,@target_id,@text)";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@reporter_id",reporter_id);
            cmd.Parameters.AddWithValue("@target_id",target_id);
            cmd.Parameters.AddWithValue("@text",text);

            cmd.ExecuteNonQuery();
            Console.WriteLine("INSERT SUCESS!!!");
        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e);
        }
        finally
        {
            _con.Close();
        }
    }
    public void UpdateReportCount(int value,int id) 
    {
        try
        {
            _con.Open();
            string query = $"UPDATE agents SET num_reports = @value WHERE id = @id";

            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Console.WriteLine("UPDATE SUCESS!!!");
        }
        catch(Exception e)
        {
            Console.WriteLine("EROR: " + e);
        }
        finally
        {
            _con.Close();
        }
    }
    public void UpdateMentionCount(int id,int value) 
    {
        try
        {
            _con.Open();
            string query = $"UPDATE agents SET num_mentions = @value WHERE id = @id";

            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Console.WriteLine("UPDATE SUCESS!!!");
        }
        catch (Exception e)
        {
            Console.WriteLine("EROR: " + e);
        }
        finally
        {
            _con.Close();
        }
    }
    public void GetReporterStats() 
    {

    }
    public void GetTargetStats() { }
    public void CreateAlert() 
    {

    }
    public void GetAlerts() { }
}