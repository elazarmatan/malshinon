using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class ManagerDal
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

    public ManagerDal()
    {
        this.createConnection();
    }

    public List<persons> GetPerson(string type)
    {

        List<persons> pers = new List<persons>(); 
        try
        {
            _con.Open();
            string query = "SELECT * FROM persons WHERE type = @type";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@type", type);
            

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                persons p = new persons(reader.GetString ("first_name"),reader.GetString("last_name"),reader.GetString("secret_code"),reader.GetString("type"),reader.GetInt32("num_reports"),reader.GetInt32("num_mentions"));
                pers.Add(p);
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
        return pers;
    }
}