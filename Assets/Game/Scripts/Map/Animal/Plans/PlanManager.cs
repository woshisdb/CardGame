using System.Collections.Generic;

public class PlanManager
{
    public Dictionary<int, List<PlanBase>> plansPerDay;
    public Dictionary<INpc, List<PlanBase>> plansPerNpc;
    public PlanManager()
    {
        plansPerDay = new Dictionary<int, List<PlanBase>>();
        plansPerNpc = new Dictionary<INpc, List<PlanBase>>();
    }

    public List<PlanBase> GetPlans(CellItem cellItem)
    {
        return cellItem.Plans;
    }
}