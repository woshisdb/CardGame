using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPlan
{
    public Npc Owner;                // 计划发起者
    public string Action;            // 行为类型
    public string Location;          // 行为地点
    public int TimeSlot;             // 回合时间段

    // 需要的关键角色及对应人数
    public Dictionary<string, int> RequiredRolesCount = new();

    // 计划当前阶段
    public PlanStage Stage;

    public bool IsConfirmed;

    // 匹配成功的角色->NPC列表
    public Dictionary<string, List<Npc>> MatchedNpcsByRole = new();
}
