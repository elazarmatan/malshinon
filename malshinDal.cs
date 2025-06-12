using System;
using System.Collections.Generic;
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



    public bool GetPersonByName(string name,string lastName)
    {
        bool result = false;
        
        try
        {
            _con.Open();            
            Console.WriteLine("CONECTION SUCESS\n");
            string query = "SELECT * FROM persons WHERE first_name=@name AND last_name=@lName";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@lName", lastName);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {           
                result = true;
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
        return result;
    }
    public bool GetPersonBySecretCode(string secreteCode)
    {
        bool result = false;
        try
        {
            _con.Open();
            Console.WriteLine("CONECTION SUCESS\n");
            string query = "SELECT * FROM persons WHERE secret_code=@secret_code";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@secret_code", secreteCode);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                result = true;
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
        return result;
    }
    public void InsertNewPerson(persons pers) 
    {
        try
        {
            _con.Open();
            string query = "INSERT INTO persons (first_name,last_name,secret_code,type)" +
                " VALUES(@first_name,@last_name,@secret_code,@type)";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@first_name",pers.first_name);
            cmd.Parameters.AddWithValue("@last_name",pers.last_name);
            cmd.Parameters.AddWithValue("@secret_code",pers.secret_code);
            cmd.Parameters.AddWithValue("@type", pers.type);



            cmd.ExecuteNonQuery();
            //Console.WriteLine("INSERT USER SUCESS!!!");
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
   
    public void updateType(int id, string value)
    {
        try
        {
            this._con.Open();
            string query = $"UPDATE persons SET type = @value WHERE id = @id";

            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@value", value);
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
   
    public int getIdByName(string firstName, string lastName)
    {
        int id = 0;
        try
        {
           
            _con.Open();
            string query = "SELECT id FROM persons WHERE first_name=@first_name AND last_name= @last_name";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@first_name", firstName);
            cmd.Parameters.AddWithValue("@last_name", lastName);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32("id");
            }
            
        }
        catch(Exception e)
        {
            Console.WriteLine("EROR: " + e.Message);
        }
        finally
        {
            _con.Close();
        }
        return id;
    }

    public int getIdBySecretCode(string secretCode)
    {
        int id = 0;
        try
        {

            _con.Open();
            string query = "SELECT id FROM persons WHERE secret_code=@secret_code";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@secret_code", secretCode);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32("id");
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
        return id;
    }
    public string createSecretCode(string name)
    {
        string secret = "";
        foreach(char c in name)
        {
          char d = (char)(219 - c);
            secret += d;
        }
        return secret;
    }

    public bool SecretCodeExists(string code)
    {
        bool exists = false;

        try
        {
            _con.Open();
            string query = "SELECT COUNT(*) FROM persons WHERE secret_code = @code";
            var cmd = new MySqlCommand(query, _con);
            cmd.Parameters.AddWithValue("@code", code);

            var result = Convert.ToInt32(cmd.ExecuteScalar());
            exists = result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR :" + e.Message);
        }
        finally
        {
            _con.Close();
        }

        return exists;
    }

    public fullName NameExtraction(string text)
    {
        List<string> texts = new List<string>(text.Split(' '));
        fullName name = new fullName();
        string firstName = "";
        string lastName = "";
        for(int i = 0;i < texts.Count; i++)
        {
            foreach(char c in texts[i])
            {
                if (char.IsUpper(c))
                {
                    firstName = texts[i];
                    name.firstName = firstName;
                    lastName = texts[i + 1];
                    name.lastName = lastName;
                    break;
                }
            }
        }
        return name;
    }

    public bool getType(string type,int id)
    {
        bool res = false;
        string typ = "";
        try
        {

            _con.Open();
            string query = "SELECT type FROM persons WHERE id = @id";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                typ = reader.GetString("type");

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
        if (typ == type)
        {
            res = true;
        }
        return res;
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