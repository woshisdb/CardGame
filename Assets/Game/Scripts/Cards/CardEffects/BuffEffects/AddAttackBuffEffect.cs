// namespace Game.Scripts.Cards.CardEffects
// {
//     public class CounterBuffEffect
//     {
//         
//     }
// }

using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AddAttackBuffEffectData : CardEffectData
{
    [ShowInInspector,SerializeField]
    protected int power;
    [ShowInInspector,SerializeField]
    protected int damage;
    [ShowInInspector,SerializeField]
    protected AddEnum type;
    public AddAttackBuffEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
    }

    public override CardEffectData Clone()
    {
        var ret = new AddAttackBuffEffectData(cardEffect);
        ret.power = power;
        ret.damage = damage;
        ret.type = type;
        return ret;
    }

    public override int GetPower()
    {
        return gameRuleProcessor.Process(ProcessType.Power,tableModel.gameRule.owner,power);
    }

    public int GetDamage()
    {
        return gameRuleProcessor.Process(ProcessType.AttackBuff,tableModel.gameRule.owner,damage);
    }
    public AddEnum GetAddType()
    {
        return type;
    }
}

[CreateAssetMenu(fileName = "AddAttackBuffEffect", menuName = "CardEffect/AddAttackBuffEffect")]
public class AddAttackBuffEffect : CardEffect, ISendEvent
{
    public AddAttackBuffEffect()
    {
        cardEffectEnum = CardEffectEnum.CounterBuffEffect;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var hero = (table.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView).cardModel as IAnimalCard;
        return Cost(effectData.GetPower(), new RemoveHandCardData(card, 
            () => {
                State.Next(
                    new AddAttackBuffToAnimal(hero, () => { State.End(done); }));
                },
            ()=>{
                State.End(done);
            })
        );
    }

    public override CardEffectData EffectData()
    {
        return new AddAttackBuffEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as AddAttackBuffEffectData;
        if (cd.GetAddType() == AddEnum.Add)
        {
            return "伤害+" + cd.GetDamage();
        }
        else
        {
            return "伤害x" + cd.GetDamage();
        }

    }
}
