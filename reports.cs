using System;

public class Reports
{
    int id { get; set; }
    string text { get; set; }
    int reportsId { get; set; }
    int targetId { get; set; }
    DateTime time { get; }

    public Reports(int id, string text, int reportsId, int targetId,DateTime time)
    {
        this.id = id;
        this.text = text;
        this.reportsId = reportsId;
        this.targetId = targetId;
        this.time = time;
    }

    public Reports(string text, int reportsId, int targetId)
    {
        this.text = text;
        this.reportsId = reportsId;
        this.targetId = targetId;
    }
}