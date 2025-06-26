using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStoreCellItem : CellItem, ISendEvent
{
    public INpc owner;
    public override List<PlanDetail> GetPlanDetails(INpc npc)
    {
        var buyPlan = new BuyPlan();
        buyPlan.Math(buyPlan.buyer,npc);
        buyPlan.Math(buyPlan.seller, owner);
        return new List<PlanDetail>() { new PlanDetail(buyPlan, true, null) };
    }
}
