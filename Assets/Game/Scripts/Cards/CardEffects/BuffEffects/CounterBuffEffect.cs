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
    public int hp;
    public CounterBuffEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
    }

    public override CardEffectData Clone()
    {
        var ret = new CounterBuffEffectData(cardEffect);
        ret.power = power;
        ret.hp = hp;
        return ret;
    }

    public override int GetPower()
    {
        return power;
    }

    public int GetHp()
    {
        return tableModel.gameRule.GameRuleProcessor.ProcessAttack(tableModel.gameRule.owner,hp);
    }
}

[CreateAssetMenu(fileName = "CounterBuffEffect", menuName = "CardEffect/CounterBuffEffect")]
public class CounterBuffEffect : CardEffect,ISendEvent
{
    public CounterBuffEffect()
    {
        cardEffectEnum = CardEffectEnum.CounterBuffEffect;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card,Action done)
    {
        var data= effectData as CounterBuffEffectData;
        var hero = (table.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView).cardModel as IAnimalCard;
        return Cost((effectData as CounterBuffEffectData).power, new AddCounterBuffToAnimal(hero, () => { State.End(done); },
            () => { return data.GetHp();}));
    }

    public override CardEffectData EffectData()
    {
        return new CounterBuffEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as CounterBuffEffectData;
        return "花费" + cd.GetPower().ToString()+"造成反伤";
    }
}
