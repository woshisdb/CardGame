using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeCellItem : CellItem, ISendEvent
{
    public override List<PlanDetail> GetPlanDetails(INpc npc)
    {
        var sleepPlan = new SleepPlan();
        sleepPlan.Math(sleepPlan.sleeper, npc);
        return new List<PlanDetail>() { new PlanDetail(sleepPlan, true, null) };
    }
}
