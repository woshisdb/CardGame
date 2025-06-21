using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : INpc
{
    public Cell cell;
    public PlanBase Plan;
    public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void AddProperty(AnimalProperty property, int val)
    {
        throw new NotImplementedException();
    }

    public void Decision(Action act)
    {
        throw new NotImplementedException();
    }

    public DialogueChoice GetChoice(DialogueNode dialogue)
    {
        throw new NotImplementedException();
    }

    public PlanBase GetPlan()
    {
        throw new NotImplementedException();
    }

    public int GetProperty(AnimalProperty property)
    {
        throw new NotImplementedException();
    }

    public Dictionary<AnimalProperty, int> GetPropertys()
    {
        throw new NotImplementedException();
    }

    public NpcRelationship GetRelationship(INpc animal)
    {
        throw new NotImplementedException();
    }

    public Shop GetShop()
    {
        throw new NotImplementedException();
    }

    public Cell InPlace()
    {
        return cell;
    }

    public bool IsPlayer()
    {
        return true;
    }

    public void SetPlan(PlanBase plan)
    {
        this.Plan = plan;
    }
}
