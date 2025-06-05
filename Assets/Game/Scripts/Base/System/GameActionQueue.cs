using System;
using System.Collections.Generic;
using UnityEngine;

public class GameActionQueue
{
    public List<Action<Action>> actions;

    public GameActionQueue()
    {
        actions = new List<Action<Action>>();
    }

    public void Add(Action<Action> action)
    {
        actions.Add(action);
    }

    public void Remove(Action<Action> action)
    {
        actions.Remove(action);
    }
}