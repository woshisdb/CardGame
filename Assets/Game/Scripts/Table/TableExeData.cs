using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class State
{
    public static void Next(TableExeData data)
    {
        data.Exe();
    }
    public static void End(Action done)
    {
        State.Next(new EndData(done));
    }
}

public abstract class TableExeData : ISendEvent
{
    public TableModel tableModel
    {
        get
        {
            return GameArchitect.Instance.GetTableModel();
        }
    }

    public abstract void Exe();
    public void EndStage()
    {
        State.End(null);
    }
    public void Send(TableEffectData tableEffectData)
    {
        this.SendEvent(new TableEffectDataEvent(tableEffectData));
    }
}

public class EndData : TableExeData
{
    public Action done;
    public EndData(Action done = null)
    {
        this.done = done;
    }
    public override void Exe()
    {
        if (done != null)
        {
            done();
        }
        else
        {
            this.SendEvent(new ChangeEvent(TableCircleEnum.Pending));
        }
    }
}

public class TableChangeHpData : TableExeData
{
    public int hpChange;
    public IAnimalCard from;
    public IAnimalCard to;
    public Action done;
    public TableChangeHpData(int hpChange, IAnimalCard from, IAnimalCard to,Action done) : base()
    {
        this.done = done;
        this.hpChange = hpChange;
        this.from = from;
        this.to = to;
    }

    public override void Exe()
    {
        if (hpChange>0)
        {
            this.SendEvent( new TableEffectDataEvent(new AddHpEffectObjData(to,hpChange,done)));
        }
        else if (hpChange < 0)
        {
            
        }
        else
        {
            done();
        }
    }
}

public class SelectSlotData : TableExeData
{
    /// <summary>
    /// Ҫ��
    /// </summary>
    public Func<SlotView, bool> require;
    /// <summary>
    /// �ɹ�ѡ��
    /// </summary>
    public Action<SlotView> onSucc;
    /// <summary>
    /// ʧ��ѡ��
    /// </summary>
    public Action onConceal;
    public SelectSlotData(Func<SlotView, bool> require, Action<SlotView> onSucc, Action onConceal = null)
    {
        this.require = require;
        this.onSucc = onSucc;
        this.onConceal = onConceal;
    }

    public override void Exe()
    {
        tableModel.StartSelectSlot((slot) => { return this.require(slot); }, onSucc);
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
    public RemoveHandCardData(CardModel cardModel, Action onSucc, Action onFail = null)
    {
        this.cardModel = cardModel;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
        GameArchitect.Instance.cardManager.RemoveCard(cardModel, onSucc, onFail == null ? EndStage : onFail);
    }
}

public class AddSlotCardData : TableExeData
{
    public Action onSucc;
    public CardModel cardModel;
    public OneCardSlotView slotView;
    public Action onFail;
    public AddSlotCardData(OneCardSlotView slotView, CardModel cardModel, Action onSucc, Action onFail = null)
    {
        this.slotView = slotView;
        this.cardModel = cardModel;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
        slotView.AddCard(cardModel, onSucc, onFail == null ? EndStage : onFail);
    }
}