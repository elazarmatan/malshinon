using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

public class LogDal
{
    string constr = "server=localhost;username=root;password=;database=malshinon";
    private MySqlConnection _con;

    private void createConnection()
    {
        this._con = new MySqlConnection(constr);

    }

    public LogDal()
    {
        this.createConnection();
    }

    private MySqlCommand comand(string query)
    {
        MySqlCommand cmd = new MySqlCommand(query, _con);
        return cmd;
    }

    public void createLog(string log)
    {
        try
        {
            _con.Open();
            string query = "INSERT INTO log (log,time)" +
                " VALUES(@log,@time)";
            var cmd = comand(query);
            cmd.Parameters.AddWithValue("@log", log);
            DateTime time = DateTime.Now;
            cmd.Parameters.AddWithValue("@time", time);

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

    public List<Log> getAllLogs()
    {
        List<Log> logs = new List<Log>();
        try
        {
            _con.Open();
            string query = "SELECT log,time FROM log";
            var cmd = comand(query);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Log l = new Log(reader.GetString("log"),reader.GetDateTime("time"));
                logs.Add(l);
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
        return logs;
    }
}
