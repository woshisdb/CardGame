using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProcesser
{
    public Func<IAnimalCard,int, int> func;
    public AttackProcesser(Func<IAnimalCard,int, int> func)
    {
        this.func = func;
    }
}

public class GameRuleProcessor
{
    public List<AttackProcesser> attackProcessers=new List<AttackProcesser>();
    public int ProcessAttack(IAnimalCard cardModel,int val)
    {
        int ret = val;
        for(int i=0;i< attackProcessers.Count; i++)
        {
            ret = attackProcessers[i].func(cardModel,ret);
        }
        return ret;
    }
    public void RegisterAttackProcess(AttackProcesser attackProcesser)
    {
        this.attackProcessers.Add(attackProcesser);
    }
    public void RemoveRegisterAttackProcess(AttackProcesser attackProcesser)
    {
        this.attackProcessers.Remove(attackProcesser);
    }
}