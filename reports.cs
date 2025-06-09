using System;

public class Reports
{
    int id { get; set; }
    string text { get; set; }
    int reportsId { get; set; }
    int targetId { get; set; }
    DateTime time { get; }

    public Reports(int id, string text, int reportsId, int targetId)
    {
        this.id = id;
        this.text = text;
        this.reportsId = reportsId;
        this.targetId = targetId;
    }
}