using System;
using System.Collections.Generic;

public class GoapState
{
    public GoapWorld GoapWorld;
    public Dictionary<string, object> Values = new Dictionary<string, object>();

    public GoapState Clone()
    {
        var clone = new GoapState();
        foreach (var kv in Values)
            clone.Values[kv.Key] = kv.Value is ICloneable c ? c.Clone() : kv.Value;
        return clone;
    }

    public void Set(string key, object value)
    {
        Values[key] = value;
    }

    public T Get<T>(string key, T defaultValue = default)
    {
        if (Values.TryGetValue(key, out object val) && val is T cast)
            return cast;
        return defaultValue;
    }

    public virtual bool Meets(GoapState goal)
    {
        foreach (var kv in goal.Values)
        {
            if (!Values.ContainsKey(kv.Key)) return false;

            var currentVal = Values[kv.Key];
            var targetVal = kv.Value;

            if (targetVal is bool boolTarget)
            {
                if (!(currentVal is bool b && b == boolTarget)) return false;
            }
            else if (targetVal is int intTarget)
            {
                if (!(currentVal is int i && i >= intTarget)) return false;
            }
        }
        return true;
    }

    public void Apply(GoapState effects)
    {
        foreach (var kv in effects.Values)
        {
            if (kv.Value is bool b)
                Values[kv.Key] = b;
            else if (kv.Value is int val)
            {
                int old = Get<int>(kv.Key);
                Values[kv.Key] =  val;
            }
        }
    }
}