using System;
using System.Collections.Generic;

public abstract class CombinePlan:PlanBase
{
    /// <summary>
    /// 前驱计划
    /// </summary>
    public List<PlanEnum> prePlans;
    /// <summary>
    /// 是否可以合并
    /// </summary>
    /// <param name="plan"></param>
    /// <param name="inputs"></param>
    /// <returns></returns>

    public abstract bool CanCombine(out PlanBase plan,params PlanBase[]  inputs);
}