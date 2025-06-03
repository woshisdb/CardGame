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
using UnityEngine;

public enum AttackAddEnum
{
    Add,
    Multi,
}

public class AddAttackBuffEffectData : CardEffectData
{
    public int power;
    public int damage;
    public AttackAddEnum type;
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
        return power;
    }
}

[CreateAssetMenu(fileName = "AddAttackBuffEffect", menuName = "CardEffect/AddAttackBuffEffect")]
public class AddAttackBuffEffect : CardEffect, ISendEvent
{
    public AddAttackBuffEffect()
    {
        cardEffectEnum = CardEffectEnum.CounterBuffEffect;
    }
    public override bool CanExe(CardEffectData effectData, TableModel table, CardModel card)
    {
        return true;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var hero = (table.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView).cardModel as IAnimalCard;
        return Cost(effectData.GetPower(), new AddAttackBuffToAnimal(hero, () => { State.End(done); }));
    }

    public override CardEffectData EffectData()
    {
        return new AddAttackBuffEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as AddAttackBuffEffectData;
        if (cd.type == AttackAddEnum.Add)
        {
            return "伤害+" + cd.damage;
        }
        else
        {
            return "伤害x" + cd.damage;
        }

    }
}
