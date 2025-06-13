using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class NpcRelationship
{
    public INpc From { get; private set; }
    public INpc To { get; private set; }

    public NpcRelationshipType Type { get; private set; }

    // 亲密度 / 好感度值，正数表示好感，负数表示仇恨
    public int Intimacy { get; private set; }

    public NpcRelationship(INpc from, INpc to, NpcRelationshipType type, int initialIntimacy = 0)
    {
        From = from;
        To = to;
        Type = type;
        Intimacy = initialIntimacy;
    }

    public void AddRelationshipType(NpcRelationshipType newType)
    {
        Type |= newType;
    }

    public void RemoveRelationshipType(NpcRelationshipType typeToRemove)
    {
        Type &= ~typeToRemove;
    }

    public bool HasRelationshipType(NpcRelationshipType checkType)
    {
        return (Type & checkType) != 0;
    }

    public void ChangeIntimacy(int amount)
    {
        Intimacy += amount;
    }
}



public interface INpc
{
    public string Name { get; set; }
    bool IsPlayer();
    void SetPlan(PlanBase plan);
    PlanBase GetPlan();
    /// <summary>
    /// 所在位置
    /// </summary>
    /// <returns></returns>
    Cell InPlace();
    /// <summary>
    /// 获得友好度[-1,1]讨厌->好感
    /// </summary>
    /// <param name="animal"></param>
    /// <returns></returns>
    NpcRelationship GetRelationship(INpc animal);
    /// <summary>
    /// 获得个人的特性
    /// </summary>
    /// <returns></returns>
    Dictionary<AnimalProperty, int> GetPropertys();
    int GetProperty(AnimalProperty property);
    void AddProperty(AnimalProperty property,int val);
    void Decision(Action act);
    
}
