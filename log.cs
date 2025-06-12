using System;

public class Log
{
    int id { get; set; }
    string log { get; set; }
    DateTime time { get; set; }
    public Log(string log,DateTime time)
    {
        this.log = log;
        this.time = time;
    }
    public override string ToString()
    {
        return $"Log Entry:\nMessage: {log}\nTime: {time}";
    }
}