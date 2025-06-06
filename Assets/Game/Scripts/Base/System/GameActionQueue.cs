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

    public void Run(Action done)
    {
        AsyncQueue queue = new AsyncQueue();
        foreach (Action<Action> action in actions)
        {
            queue.Add(action);
        }
        queue.Add(e =>
        {
            done?.Invoke();
            e();
        });
        queue.Run(done);
    }
}