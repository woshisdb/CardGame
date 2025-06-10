
using System;
using System.Collections.Generic;

public class GoapWorld
{
    public Dictionary<string, object> Values = new Dictionary<string, object>();

    public GoapWorld Clone()
    {
        var clone = new GoapWorld();
        foreach (var kv in Values)
            clone.Values[kv.Key] = kv.Value is ICloneable c ? c.Clone() : kv.Value;
        return clone;
    }
}