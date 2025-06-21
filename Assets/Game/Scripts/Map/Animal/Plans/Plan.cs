using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanEnum
{
    Sleep,
    Test2,
}

public abstract class DialogueStoryBuilder
{
    protected DialogueEnvir envir;
    public DialogueStoryBuilder(DialogueEnvir envir)
    {
        this.envir = envir;
    }
    public abstract DialogueNode build();
}

public abstract class PlanBase
{
    public PlanEnum PlanEnum;
    public bool visible;//是否可以全局查看
    public INpc Owner;                // 计划发起者
    public CellItem Location;          // 行为地点
    public int TimeSlot;             // 回合时间段

    // 需要的关键角色及对应人数
    public Dictionary<string, int> RequiredRolesCount = new Dictionary<string, int>();

    // 匹配成功的角色->NPC列表
    public Dictionary<string, List<INpc>> MatchedNpcsByRole = new Dictionary<string, List<INpc>>();

    public abstract bool CanRun();
    public abstract void Run(Action done);
    public INpc GetNpc(string name)
    {
        return MatchedNpcsByRole[name][0];
    }
    public void Math(string str,INpc npc)
    {
        if(MatchedNpcsByRole.ContainsKey(str))
        {
            MatchedNpcsByRole[str].Add(npc);
        }
        else
        {
            MatchedNpcsByRole[str] = new List<INpc>();
            MatchedNpcsByRole[str].Add(npc);
        }
    }
    public List<INpc> GetNpcs(string name)
    {
        return MatchedNpcsByRole[name];
    }
}
