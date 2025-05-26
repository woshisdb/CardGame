using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkillEffectData : CardEffectData
{
    public int hp;
    public AttackSkillEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
    }
}

[CreateAssetMenu(fileName = "AttackSkillEffect", menuName = "Effect/AttackSkillEffect")]
public class AttackSkillEffect : CardEffect, ISendEvent
{
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
        return new ChangePowerData(() => {
            State.Next(
            new RemoveHandCardData(card, () => {
                State.Next(
                new SelectSlotData((slot) =>
                {
                    return slot is OneCardSlotView && !((OneCardSlotView)slot).IsEmpty();
                },
                (slot) =>
                {
                    State.Next(new TableChangeHpData(eff.hp, null, (slot as OneCardSlotView).cardModel as IAnimalCard, done));
                }));

            })
            );
        }, eff.hp);
    }

    public override CardEffectData EffectData()
    {
        return new AttackSkillEffectData(this);
    }
}