using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : INpc
{
    public PlanBase Plan;
    public string name = "tester";
    public Shop shop;
    public Cell cell;
    public string Name { get => name; set => this.name =value; }

    public void AddProperty(AnimalProperty property, int val)
    {
        throw new NotImplementedException();
    }

    public void Decision(Action act)
    {
        act();
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
        return null;
    }

    public Shop GetShop()
    {
        return shop;
    }

    public Cell InPlace()
    {
        return cell;
    }

    public bool IsPlayer()
    {
        return false;
    }

    public void SetPlan(PlanBase plan)
    {
        this.Plan = plan;
    }
    public Npc()
    {
        this.shop = new Shop();
    }
}
