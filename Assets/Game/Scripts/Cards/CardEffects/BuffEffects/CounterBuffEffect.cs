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

public class CounterBuffEffectData: CardEffectData
{
    public int power;
    public CounterBuffEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
    }

    public override CardEffectData Clone()
    {
        var ret = new CounterBuffEffectData(cardEffect);
        ret.power = power;
        return ret;
    }

    public override int GetPower()
    {
        return power;
    }
}

[CreateAssetMenu(fileName = "CounterBuffEffect", menuName = "CardEffect/CounterBuffEffect")]
public class CounterBuffEffect : CardEffect,ISendEvent
{
    public CounterBuffEffect()
    {
        cardEffectEnum = CardEffectEnum.CounterBuffEffect;
    }
    public override bool CanExe(CardEffectData effectData, TableModel table, CardModel card)
    {
        return true;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card,Action done)
    {
        var hero = (table.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView).cardModel as IAnimalCard;
        return Cost((effectData as CounterBuffEffectData).power, new AddCounterBuffToAnimal(hero, () => { State.End(done); }));
    }

    public override CardEffectData EffectData()
    {
        return new CounterBuffEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as CounterBuffEffectData;
        return "添加效果:反伤"+",每次造成" + cd.GetPower().ToString()+"点伤害";
    }
}
