using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkillEffectData : CardEffectData
{
    public int hp;
    public int power;
    public AttackSkillEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
        var data = cardEffect as AttackSkillEffect;
        hp = data.hp;
        power = data.power;
    }
}

[CreateAssetMenu(fileName = "AttackSkillEffect", menuName = "Effect/AttackSkillEffect")]
public class AttackSkillEffect : CardEffect, ISendEvent
{
    public int hp;
    public int power;
    public AttackSkillEffect()
    {
        cardEffectEnum = CardEffectEnum.AddBuffEffect;
    }
    public override bool CanExe(CardEffectData effectData, TableModel table, CardModel card)
    {
        return true;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var eff = effectData as AttackSkillEffectData;
        return Cost(eff.power,new RemoveHandCardData(card, () => {
                State.Next(
                new SelectSlotData((slot) =>
                {
                    return slot is OneCardSlotView && !((OneCardSlotView)slot).IsEmpty();
                },
                (slot) =>
                {
                    State.Next(new TableChangeHpData(eff.hp, null, (slot as OneCardSlotView).cardModel as IAnimalCard, done));
                }));
            }, ()=>{}));
    }
    public override CardEffectData EffectData()
    {
        return new AttackSkillEffectData(this);
    }
}