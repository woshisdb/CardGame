using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanDetail
{
    public PlanBase Plan;
    public bool canProcess;
    public string tipInfo;
    public PlanDetail(PlanBase plan, bool canProcess, string tipInfo)
    {
        Plan = plan;
        this.canProcess = canProcess;
        this.tipInfo = tipInfo;
    }
}

public abstract class CellItem : MonoBehaviour, IPointerClickHandler, ISendEvent, IUISelector
{
    public Cell cell;
    [ShowInInspector]
    public List<PlanBase> Plans=new List<PlanBase>();
    public Player Player { get { return GameArchitect.Instance.player; } }
    public HeroCardModel Hero { get { return GameArchitect.Instance.heroCardModel; } }
    public virtual List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        var plans = GetPlanDetails(Player);
        foreach(var plan in plans)
        {
            if(!plan.canProcess&&plan.tipInfo==null)
            {
                continue;
            }
            else
            {
                if(plan.canProcess)
                {
                   ret.Add( new ButtonBinder(() => { return plan.Plan.PlanEnum.ToString(); }, 
                   () => {
                       Plans.Add(plan.Plan);
                       GameArchitect.Instance.storyManager.OnPlayerSelectPlan?.Invoke();
                   }));
                }
                else
                {
                    ret.Add(new ButtonBinder(() => { return plan.Plan.PlanEnum.ToString()+"["+plan.tipInfo+"]"; }, () => { }));
                }
            }
        }
        return ret;
    }
    public abstract List<PlanDetail> GetPlanDetails(INpc npc);

    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        this.SendEvent<SelectViewEvent>(new SelectViewEvent(this));
        //Debug.Log(1111);
        //this.SendEvent<SelectViewEvent>(new SelectViewEvent());
    }
}
