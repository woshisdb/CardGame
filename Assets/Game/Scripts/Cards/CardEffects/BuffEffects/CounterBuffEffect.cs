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
        return Cost((effectData as CounterBuffEffectData).power, new AddBuffToAnimal(hero, () => { State.End(done); }));
    }

    public override CardEffectData EffectData()
    {
        return new CounterBuffEffectData(this);
    }
}
