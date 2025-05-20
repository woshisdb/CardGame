using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public static class State
{
    public static void Next(TableExeData data)
    {
        data.Exe();
    }
    public static void End()
    {
        State.Next(new EndData());
    }
}

public abstract class TableExeData:ISendEvent
{
    public abstract void Exe();
    public void EndStage()
    {
        State.End();
    }
}

public class EndData : TableExeData
{
    public override void Exe()
    {
        this.SendEvent(new ChangeEvent(TableCircleEnum.Pending));
    }
}

public class SelectSlotData : TableExeData
{
    /// <summary>
    /// 要求
    /// </summary>
    public Func<SlotView,bool> require;
    /// <summary>
    /// 成功选择
    /// </summary>
    public Action<SlotView> onSucc;
    /// <summary>
    /// 失败选择
    /// </summary>
    public Action onConceal;
    public SelectSlotData(Func<SlotView,bool> require, Action<SlotView> onSucc, Action onConceal=null)
    {
        this.require = require;
        this.onSucc = onSucc;
        this.onConceal = onConceal;
    }

    public override void Exe()
    {
        var table = GameArchitect.Instance.GetTableModel();
        table.StartSelectSlot((slot) => { return this.require(slot); },onSucc);
    }
}

public class AddHandCardData : TableExeData
{
    public Action onSucc;
    public CardModel cardModel;
    public Action onFail;
    public AddHandCardData(CardModel cardModel, Action onSucc, Action onFail = null)
    {
        this.cardModel = cardModel;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
        GameArchitect.Instance.cardManager.AddCard(cardModel, onSucc, onFail == null ? EndStage : onFail);
    }
}

public class RemoveHandCardData : TableExeData
{
    public Action onSucc;
    public CardModel cardModel;
    public Action onFail;
    public RemoveHandCardData(CardModel cardModel,Action onSucc, Action onFail=null)
    {
        this.cardModel = cardModel;
        this.onSucc=onSucc;
        this.onFail=onFail;
    }
    public override void Exe()
    {
        GameArchitect.Instance.cardManager.RemoveCard(cardModel,onSucc, onFail == null ? EndStage : onFail);
    }
}

public class AddSlotCardData : TableExeData
{
    public Action onSucc;
    public CardModel cardModel;
    public OneCardSlotView slotView;
    public Action onFail;
    public AddSlotCardData(OneCardSlotView slotView,CardModel cardModel, Action onSucc, Action onFail=null)
    {
        this.slotView = slotView;
        this.cardModel = cardModel;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
       slotView.AddCard(cardModel, onSucc, onFail==null? EndStage : onFail);
    }
}

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
            State.Next(e.effect.Effect(e.effect.EffectData(), this, e.cardModel));
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
        this.Register<SelectSlotEvent>(SelectSlotEvent);
        gameRule.Init();
    }
    public void Destory()
    {
        this.Unregister<ChangeEvent>(ChangeEvent);
        this.Unregister<CardEffectEvent>(CardEffectEvent);
        this.Unregister<SelectSlotEvent>(SelectSlotEvent);
    }
    public void StartSelectSlot(Func<SlotView,bool> cond,Action<SlotView> succ)
    {
        slotSel = new SlotSel(cond, succ);
        this.SendEvent(new ChangeEvent(TableCircleEnum.SelectSloting));
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
