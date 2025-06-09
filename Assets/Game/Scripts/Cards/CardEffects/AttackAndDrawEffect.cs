using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 攻击后再抽牌
/// </summary>
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
        var data = cardEffect as AttackAndDrawEffectData;
        attack = data.attack;
        power = data.power;
        drawCard = data.drawCard;
    }

    public override CardEffectData Clone()
    {
       var ret = new AttackAndDrawEffectData(cardEffect);
       ret.attack = attack;
       ret.power = power;
        ret.drawCard = drawCard;
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
    public int GetAttack()
    {
        return gameRuleProcessor.Process(ProcessType.Attack,tableModel.gameRule.owner,attack);
    }
}

[CreateAssetMenu(fileName = "AttackAndDrawEffect", menuName = "CardEffect/AttackAndDrawEffect")]
public class AttackAndDrawEffect : CardEffect, ISendEvent
{
    public AttackAndDrawEffect()
    {
        cardEffectEnum = CardEffectEnum.AttackAndDraw;
    }
    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var eff = effectData as AttackAndDrawEffectData;
        Action onChangeHp = () =>
        {
            State.Next(new GetCardFromDeck(eff.DrawCard(),done,done));
        };
        Action<SlotView> onSelectSlot = (slot) =>
        {
            State.Next(new TableChangeHpData(eff.GetAttack(), null,
                (slot as OneCardSlotView).cardModel as IAnimalCard, onChangeHp));
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
        var str = "";
        if(cd.GetAttack() < 0)
            str = "造成" + Math.Abs( cd.GetAttack() )+"点伤害";
        else if(cd.GetAttack() > 0)
        {
            str = "回复" + Math.Abs(cd.GetAttack() )+ "点生命";
        }
        else
        {
            str = "什么都不做";
        }
        str += ",并且抽"+cd.DrawCard()+ "牌";
        return str;
    }
}