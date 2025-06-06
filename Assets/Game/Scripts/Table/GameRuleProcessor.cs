using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProcesser
{
}

public class Processer<T> : IProcesser
{
    private List<Func<IAnimalCard, T, T>> funcs = new List<Func<IAnimalCard, T, T>>();

    public T Process(IAnimalCard card, T val)
    {
        T result = val;
        foreach (var func in funcs)
            result = func(card, result);
        return result;
    }

    public void Register(Func<IAnimalCard, T, T> func) => funcs.Add(func);
    public void Remove(Func<IAnimalCard, T, T> func) => funcs.Remove(func);
}


public enum ProcessType
{
    Attack
}

public class GameRuleProcessor
{
    private Dictionary<ProcessType, IProcesser> _processors = new Dictionary<ProcessType, IProcesser>();

    public void Register<T>(ProcessType type, Func<IAnimalCard, T, T> func)
    {
        if (!_processors.ContainsKey(type))
            _processors[type] = new Processer<T>();

        ( _processors[type] as Processer<T> )?.Register(func);
    }

    public T Process<T>(ProcessType type, IAnimalCard card, T val)
    {
        if (_processors.TryGetValue(type, out var processor))
        {
            var typedProcessor = processor as Processer<T>;
            if (typedProcessor != null)
            {
                return typedProcessor.Process(card, val);
            }
        }
        return val;
    }
    public void Remove<T>(ProcessType type, Func<IAnimalCard, T, T> func)
    {
        if (_processors.TryGetValue(type, out var processor))
        {
            (processor as Processer<T>)?.Remove(func);
        }
    }
}
