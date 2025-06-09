using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AttackAndAddHpData : CardEffectData
{
    [ShowInInspector,SerializeField]
    protected int attack;
    [ShowInInspector,SerializeField]
    protected int power;
    [ShowInInspector,SerializeField]
    protected int hp;
    public AttackAndAddHpData(ICardEffect cardEffect) : base(cardEffect)
    {
        var data = cardEffect as AttackAndAddHp;
        attack = data.attack;
        power = data.power;
        hp = data.hp;
    }

    public override CardEffectData Clone()
    {
       var ret = new AttackAndAddHpData(cardEffect);
       ret.attack = attack;
       ret.power = power;
       ret.hp = hp;
       return ret;
    }

    public override int GetPower()
    {
        return gameRuleProcessor.Process(ProcessType.Power,tableModel.gameRule.owner,power);//power;
    }

    public int GetAttack()
    {
        return gameRuleProcessor.Process(ProcessType.Attack,tableModel.gameRule.owner,attack);//power;
    }
    public int GetHp()
    {
        return gameRuleProcessor.Process(ProcessType.Attack,tableModel.gameRule.owner,attack);
    }
}

[CreateAssetMenu(fileName = "AttackAndAddHp", menuName = "CardEffect/AttackAndAddHp")]
public class AttackAndAddHp : CardEffect, ISendEvent
{
    public int attack;
    public int hp;
    public int power;
    public AttackAndAddHp()
    {
        cardEffectEnum = CardEffectEnum.AttackAndAddHp;
    }
    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var eff = effectData as AttackAndAddHpData;
        Action onAttackSucc = () =>
        {
            State.Next(new TableChangeHpData(eff.GetHp(), null,
                GameArchitect.Instance.heroCardModel, done));
        };
        Action<SlotView> onSelectSlot = (slot) =>
        {
            State.Next(new TableChangeHpData(eff.GetHp(), null,
                (slot as OneCardSlotView).cardModel as IAnimalCard, onAttackSucc));
        };
        
        Action onRemoveCardSucc = () =>
        {
            State.Next(
                new SelectSlotData((slot) =>
                {
                    return slot is OneCardSlotView && !((OneCardSlotView)slot).IsEmpty();
                },
                onSelectSlot)
            );
        };
        return Cost(eff.GetPower(),new RemoveHandCardData(card,onRemoveCardSucc , () => { done();}));
    }
    public override CardEffectData EffectData()
    {
        return new AttackAndAddHpData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as AttackAndAddHpData;
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