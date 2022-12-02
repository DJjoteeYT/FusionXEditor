using System.Collections.Generic;

namespace FusionX_Editor.Editor.EventEditor;

public class FEventGroup
{
    public List<FEventAction> Actions = new List<FEventAction>();
    public List<FEventCondition> Conditions = new List<FEventCondition>();
}