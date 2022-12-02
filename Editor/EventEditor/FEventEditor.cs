using System;
using System.Collections.Generic;
using System.Linq;

namespace FusionX_Editor.Editor.EventEditor;

public class FEventEditor
{
    public List<FEventObject> Objects = new();
    public List<FEventGroup> Events = new();
    
    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public void Init()
    {
        Objects.Clear();
        Events.Clear();
        for (int i = 0; i < 10; i++)
        {
            CreateEventObject();
            CreateEventGroup();
        }
    }

    public void CreateEventObject()
    {
        var newObj = new FEventObject();
        Objects.Add(newObj);
    }

    public void CreateEventGroup()
    {
        var newEvGroup = new FEventGroup();
        newEvGroup.Conditions.Add(new FEventCondition(){Text = RandomString(5)});
        newEvGroup.Conditions.Add(new FEventCondition(){Text = RandomString(5)});
        if(random.Next(0,1000)>700)
            newEvGroup.Actions.Add(new FEventAction());
        Events.Add(newEvGroup);
    }
}