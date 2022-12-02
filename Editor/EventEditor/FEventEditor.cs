using System.Collections.Generic;

namespace FusionX_Editor.Editor.EventEditor;

public class FEventEditor
{
    public List<FEventObject> Objects = new List<FEventObject>();
    public List<FEventGroup> Events = new List<FEventGroup>();

    public void Init()
    {
        for (int i = 0; i < 50; i++)
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
        Events.Add(newEvGroup);
    }
}