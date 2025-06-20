﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStoreCellItem : CellItem, ISendEvent
{
    public INpc owner;
    public override List<PlanDetail> GetPlanDetails(INpc npc)
    {
        var sleepPlan = new SleepPlan();
        sleepPlan.Math(sleepPlan.sleeper,npc);
        return new List<PlanDetail>() { new PlanDetail(sleepPlan, true, null) };
    }
}
