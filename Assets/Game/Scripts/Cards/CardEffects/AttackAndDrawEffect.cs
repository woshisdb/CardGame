using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AttackAndDrawEffectData : CardEffectData
{
    [ShowInInspector,SerializeField]
    protected int attack;
    [ShowInInspector,SerializeField]
    protected int power;
    [ShowInInspector,SerializeField]
    protected int drawCard;
    public AttackAndDrawEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
        var data = cardEffect as AttackAndDrawEffect;
        attack = data.hp;
        power = data.power;
    }

    public override CardEffectData Clone()
    {
       var ret = new AttackAndDrawEffectData(cardEffect);
       ret.attack = attack;
       ret.power = power;
       return ret;
    }

    public override int GetPower()
    {
        return gameRuleProcessor.Process(ProcessType.Power,tableModel.gameRule.owner,power);//power;
    }

    public int DrawCard()
    {
        return gameRuleProcessor.Process(ProcessType.DrawCard,tableModel.gameRule.owner,drawCard);//power;
    }
    public int GetHp()
    {
        return gameRuleProcessor.Process(ProcessType.Attack,tableModel.gameRule.owner,attack);
    }
}

[CreateAssetMenu(fileName = "AttackAndDrawEffect", menuName = "CardEffect/AttackAndDrawEffect")]
public class AttackAndDrawEffect : CardEffect, ISendEvent
{
    public int hp;
    public int power;
    public AttackAndDrawEffect()
    {
        cardEffectEnum = CardEffectEnum.ChangeHp;
    }
    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var eff = effectData as AttackAndDrawEffectData;
        Action<SlotView> onSelectSlot = (slot) =>
        {
            State.Next(new TableChangeHpData(eff.GetHp(), null,
                (slot as OneCardSlotView).cardModel as IAnimalCard, done));
        };
        Action onRemoveHandCard = () =>
        {
            State.Next(new SelectSlotData((slot) =>{
                        return slot is OneCardSlotView && !((OneCardSlotView)slot).IsEmpty();
                    },
                onSelectSlot));
        };
        return Cost(eff.GetPower(),new RemoveHandCardData(card,onRemoveHandCard , ()=>{}));
    }
    public override CardEffectData EffectData()
    {
        return new AttackAndDrawEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as AttackAndDrawEffectData;
        if(cd.GetHp() < 0)
        return "造成"+ Math.Abs( cd.GetHp() )+"点伤害";
        else if(cd.GetHp() > 0)
        {
            return "回复" + Math.Abs(cd.GetHp() )+ "点生命";
        }
        else
        {
            return "什么都不做";
        }
    }
}