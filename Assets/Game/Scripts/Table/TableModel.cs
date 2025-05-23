using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class TableModel:IModel,IRegisterEvent,ISendEvent
{
    public List<SlotView> slots;
    public TableView view;
    public TableCircle tableCircle;
    public SlotSel slotSel;
    public GameRule gameRule;
    public CardManager cardManager { get
        {
            return GameArchitect.Instance.cardManager;
        } }
    public TableCircleEnum circleEnum { get { return tableCircle.circleEnum; } }

    public class SlotSel
    {
        public Func<SlotView, bool> cond;
        public Action<SlotView> succ;
        public SlotSel(Func<SlotView, bool> cond, Action<SlotView> succ)
        {
            this.cond = cond;
            this.succ = succ;
        }
    }
    public void TableEffectEvent(TableEffectDataEvent effectData)
    {
        var effs = view.effects;
        effs[effectData.tableEffectData.GetEffectEnum()].ShowEffect(effectData.tableEffectData);
    }
    public void ChangeEvent(ChangeEvent e)
    {
        tableCircle.SetCircle(e.circleEnum);
        if (e.circleEnum == TableCircleEnum.Pending)
        {
            if(gameRule != null)
            gameRule.Run();
        }
    }
    public void CardEffectEvent(CardEffectEvent e)
    {
        if (e.effect.CanExe(e.effect.EffectData(), this, e.cardModel))
        {
            State.Next(e.effect.Effect(e.effect.EffectData(), this, e.cardModel,null));
        }
    }
    public void SlotEffectEvent(SlotEffectEvent e)
    {
        if (e.effect.CanExe(e.effect.EffectData(), this, e.slotView))
        {
            State.Next(e.effect.Effect(e.effect.EffectData(), this, e.slotView,null));
        }
    }
    public void SelectSlotEvent(SelectSlotEvent e)
    {
        if (TableCircleEnum.SelectSloting == circleEnum)
        {
            if (slotSel.cond(e.slot))
            {
                slotSel.succ(e.slot);
                slotSel = null;
            }
        }
    }
    
    
    public TableModel()
    {
        tableCircle = new TableCircle();
        slots = new List<SlotView>();
        gameRule = new GameRule(this);
    }
    public void Init()
    {
        gameRule = new GameRule(this);
        //Debug.Log(gameRule);
        //Debug.Log(this);
        this.Register<ChangeEvent>(ChangeEvent);
        this.Register<CardEffectEvent>(CardEffectEvent);
        this.Register<SlotEffectEvent>(SlotEffectEvent);
        this.Register<SelectSlotEvent>(SelectSlotEvent);
        this.Register<TableEffectDataEvent>(TableEffectEvent);
        gameRule.Init();
    }
    public void Destory()
    {
        this.Unregister<ChangeEvent>(ChangeEvent);
        this.Unregister<CardEffectEvent>(CardEffectEvent);
        this.Unregister<SelectSlotEvent>(SelectSlotEvent);
        this.Unregister<SlotEffectEvent>(SlotEffectEvent);
        this.Unregister<TableEffectDataEvent>(TableEffectEvent);
    }
    public void StartSelectSlot(Func<SlotView,bool> cond,Action<SlotView> succ)
    {
        slotSel = new SlotSel(cond, succ);
        this.SendEvent(new ChangeEvent(TableCircleEnum.SelectSloting));
    }
    public SlotView FindSlotByTag(SlotTag slotTag,Func<SlotView,bool> func=null)
    {
        for(int i=0;i< slots.Count;i++)
        {
            if(slots[i].ExistTag(slotTag))
            {
                if(func!=null&& func(slots[i])||func==null)
                {
                    return slots[i];
                }
            }
        }
        return null;
    }

    public List<SlotView> FindSlotByTags(SlotTag slotTag, Func<SlotView, bool> func = null)
    {
        List<SlotView> ret = new List<SlotView>();
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].ExistTag(slotTag))
            {
                if (func != null && func(slots[i]))
                {
                    ret.Add(slots[i]);
                }
            }
        }
        return ret;
    }

    [Button]
    public void AddSlot(SlotView slotView)
    {
        var gameObj = GameObject.Instantiate(slotView.gameObject);
        gameObj.transform.SetParent(view.transform);
        gameObj.transform.localPosition = Vector3.zero;
        gameObj.GetComponent<SlotView>().tableView = view;
        slots.Add(slotView);
    }
    public IView CreateView()
    {
        return view;
    }

}
