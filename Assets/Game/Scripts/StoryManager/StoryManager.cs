using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
public class StoryManager:SerializedMonoBehaviour
{
    private DayDates day = new DayDates();
    public void Init()
    {
        GameArchitect.Instance.gameDateSystem.OnActionChanged.Add(e =>
        {
            var time = GetTime();
            ProcessAction(time.Item1,time.Item2,time.Item3,e);
        });
        GameArchitect.Instance.gameDateSystem.OnDateChanged.Add(e =>
        {
            var time = GetTime();
            ProcessDay(time.Item1,time.Item2,e);
        });
    }
    public (int, WeekDayType, DayTimeType) GetTime()
    {
        return (GameArchitect.Instance.gameDateSystem.GetCurrentWeek(),
            GameArchitect.Instance.gameDateSystem.GetWeekDayType(),
            GameArchitect.Instance.gameDateSystem.GetProgressDisplayInt()
            );
    }
    /// <summary>
    /// 当天开始的行为
    /// </summary>
    /// <param name="week"></param>
    /// <param name="day"></param>
    public void ProcessDay(int week,WeekDayType day,Action done)
    {
        
    }
    /// <summary>
    /// 一个行为结束后的检测
    /// </summary>
    /// <param name="week"></param>
    /// <param name="day"></param>
    /// <param name="dayTime"></param>
    public void ProcessAction(int week,WeekDayType day,DayTimeType dayTime,Action done)
    {
        AsyncQueue async = new AsyncQueue();
        async.Add(e =>
        {
            NpcFreeAction(e);
        });
        async.Add(e =>
        {
            CellPlanAction(e);
        });
    }

    public void CellPlanAction(Action done)
    {
        GameActionQueue actions = new GameActionQueue();
        foreach (var cell in GameArchitect.Instance.cellList)
        {
            foreach (var cellItem in cell.CellItems)
            {
                foreach (var plan in cellItem.Plans)
                {
                    actions.Add(e =>
                    {
                        if (plan.CanRun())
                        {
                            plan.Run(e);
                        }
                        e();
                    });
                }
            }
        }
        actions.Run(done);
    }
    public void NpcFreeAction(Action done)
    {
        AsyncQueue async = new AsyncQueue();
        foreach (var npc in GameArchitect.Instance.npcSetManager.npcs)
        {
            async.Add(e =>
            {
                npc.Decision(e);
            });
        }
        async.Add(e =>
        {
            done();
            e();
        });
    }
}
/// <summary>
/// 故事生成
/// </summary>
public class StoryCellItemManager
{
    public List<INpc> Npcs;
    public HashSet<PlanBase> Plans;
    public Dictionary<string, object> infos;
    public StoryCellItemManager(List<INpc> npcs)
    {
        this.Npcs = npcs;
    }

    public void ProcessNpcs()
    {
        foreach (var npc in Npcs)
        {
            
        }
    }
}