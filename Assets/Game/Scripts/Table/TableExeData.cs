using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class State
{
    public static TableModel tableModel
    {
        get
        {
            return GameArchitect.Instance.GetTableModel();
        }
    }
    public static void Next(TableExeData data)
    {
        tableModel.ExeActionBef(data,() =>
        {
            data.Exe(); 
        });
    }

    public static Action Then(TableExeData data,Action done)
    {
        return () => { tableModel.ExeActionAfter(data,done); };
    }
    
    public static void End(Action done)
    {
        State.Next(new EndData(done));
    }
}

public abstract class TableExeData : ISendEvent
{
    public bool needLink;
    public TableModel tableModel
    {
        get
        {
            return GameArchitect.Instance.GetTableModel();
        }
    }

    public TableExeData(bool needLink)
    {
        this.needLink = needLink;
    }
    public abstract void Exe();
    public void EndStage()
    {
        State.End(null);
    }

    public Action OnFail(Action onFail)
    {
        return onFail == null ? EndStage : onFail;
    }
    public void Send(TableEffectData tableEffectData)
    {
        this.SendEvent(new TableEffectDataEvent(tableEffectData));
    }
}

public class EndData : TableExeData
{
    public Action done;
    public EndData(Action done = null,bool needLink=true):base(needLink)
    {
        this.done = done;
    }
    public override void Exe()
    {
        if (done != null)
        {
            State.Then(this,done).Invoke();
        }
        else
        {
            this.SendEvent(new ChangeEvent(TableCircleEnum.Pending));//继续下个回合
        }
    }
}



public class TableChangeHpData : TableExeData
{
    public Action done;
    public int hpChange;
    public IAnimalCard from;
    public IAnimalCard to;
    public TableChangeHpData(int hpChange, IAnimalCard from, IAnimalCard to,Action done,bool needLink=true):base(needLink)
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
            this.SendEvent( new TableEffectDataEvent(new AddHpEffectObjData(to,hpChange,State.Then(this,done)
            )));
        }
        else if (hpChange < 0)
        {
            this.SendEvent(new TableEffectDataEvent(new AddHpEffectObjData(to, hpChange, State.Then(this,done))));
        }
        else
        {
            State.Then(this,done).Invoke();
        }
    }
}
/// <summary>
/// 反击BUFF
/// </summary>
public class AddCounterBuffToAnimal : TableExeData
{
    public IAnimalCard card;
    public Action done;
    public AddCounterBuffToAnimal(IAnimalCard card,Action action,bool needLink=true):base(needLink)
    {
        this.card = card;
        this.done = action;
    }

    public override void Exe()
    {
        this.SendEvent(new TableEffectDataEvent(new CounterBuffObjData(done,card,1)));
    }
}
/// <summary>
/// 加攻BUFF
/// </summary>
public class AddAttackBuffToAnimal : TableExeData
{
    public IAnimalCard card;
    public Action done;
    public AddAttackBuffToAnimal(IAnimalCard card, Action action, bool needLink = true) : base(needLink)
    {
        this.card = card;
        this.done = action;
    }

    public override void Exe()
    {
        this.SendEvent(new TableEffectDataEvent(new AttackBuffObjData(done, card, 1, e =>
        {
            return e * 2;
        })));
    }
}
/// <summary>
/// 抽卡
/// </summary>
public class GetCardFromDeck : TableExeData
{
    /// <summary>
    /// 选择对数目
    /// </summary>
    public int num;
    public Action onSucc;
    public Action onFail;

    public GetCardFromDeck(int num, Action onSucc, Action onFail,bool needLink=true):base(needLink)
    {
        this.num = num;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
        var slot = tableModel.FindSlotByName("cardDeckSlot") as CardDeckSlot;
        var cards= slot.GetCardsAnim(num,()=>{});
        if (cards == null)
        {
            State.Then(this,onFail).Invoke();
        }
        else
        {
            tableModel.cardManager.AddCardsAnim(cards, State.Then(this,onSucc));
        }
    }
}
/// <summary>
/// 选择Slot
/// </summary>
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
    public SelectSlotData(Func<SlotView, bool> require, Action<SlotView> onSucc, Action onConceal = null,bool needLink=true):base(needLink)
    {
        this.require = require;
        this.onSucc = onSucc;
        this.onConceal = onConceal;
    }

    public override void Exe()
    {
        tableModel.StartSelectSlot((slot) => { return this.require(slot); }, (e)=>
        {
            State.Then(this,()=>
            {
                onSucc(e);
            }).Invoke();
        });
    }
}
/// <summary>
/// 添加手牌
/// </summary>
public class AddHandCardData : TableExeData
{
    public Action onSucc;
    public CardModel cardModel;
    public Action onFail;
    public AddHandCardData(CardModel cardModel, Action onSucc, Action onFail = null,bool needLink=true):base(needLink)
    {
        this.cardModel = cardModel;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
        GameArchitect.Instance.cardManager.AddCard(cardModel, State.Then(this,onSucc), OnFail(onFail));
    }
}
/// <summary>
/// 移除手牌
/// </summary>
public class RemoveHandCardData : TableExeData
{
    public Action onSucc;
    public CardModel cardModel;
    public Action onFail;
    public RemoveHandCardData(CardModel cardModel, Action onSucc, Action onFail = null,bool needLink=true):base(needLink)
    {
        this.cardModel = cardModel;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
        GameArchitect.Instance.cardManager.RemoveCard(cardModel, State.Then(this,onSucc),
            OnFail(onFail)); //onFail == null ? EndStage : onFail);
    }
}

public class ChangePowerData:TableExeData
{
    public Action done;
    public int power;
    public ChangePowerData(Action done,int power,bool needLink=true):base(needLink)
    {
        this.done = done;
        this.power = power;
    }

    public override void Exe()
    {
        tableModel.gameRule.ChangePower(power);
        State.Then(this,done).Invoke();
    }
}

public class AddSlotCardData : TableExeData
{
    public Action onSucc;
    public CardModel cardModel;
    public OneCardSlotView slotView;
    public Action onFail;
    public AddSlotCardData(OneCardSlotView slotView, CardModel cardModel, Action onSucc, Action onFail = null,bool needLink=true):base(needLink)
    {
        this.slotView = slotView;
        this.cardModel = cardModel;
        this.onSucc = onSucc;
        this.onFail = onFail;
    }
    public override void Exe()
    {
        slotView.AddCard(cardModel, State.Then(this,onSucc), OnFail(onFail));
    }
}